using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

using AutoMapper;

using MVCBase.Enums;
using MVCModel;
using MVCModel.Models;
using MVCDTO;
using MVCCore.Repositories;
using MVCCore.Services;


namespace MVCService
{
    public enum SaveRelativeOption
    {
        Undo = -1,
        Update = 1
    }

    public class GenericService<TEntity, TDto, TPrimitiveDto> : BaseService, IGenericService<TEntity, TDto, TPrimitiveDto>

        where TEntity : class, IPrimitiveEntity, IBaseEntity, new()
        where TDto : class, TPrimitiveDto
        where TPrimitiveDto : BaseDTO, IPrimitiveEntity, IPrimitiveDTO, new()
    {

        private readonly IGenericRepository<TEntity> genericRepository;


        private readonly string functionNamePostSaveValidate;
        private readonly string functionNameSaveRelative;

        private readonly GlobalEnums.NmvnTaskID nmvnTaskID;

        public GenericService(IGenericRepository<TEntity> genericRepository)
            : this(genericRepository, null)
        { }

        public GenericService(IGenericRepository<TEntity> genericRepository, string functionNamePostSaveValidate)
            : this(genericRepository, functionNamePostSaveValidate, null)
        { }

        public GenericService(IGenericRepository<TEntity> genericRepository, string functionNamePostSaveValidate, string functionNameSaveRelative)
            : base(genericRepository)
        {
            this.genericRepository = genericRepository;

            this.functionNamePostSaveValidate = functionNamePostSaveValidate;
            this.functionNameSaveRelative = functionNameSaveRelative;

            this.nmvnTaskID = (new TPrimitiveDto()).NMVNTaskID;
        }


        public virtual TEntity GetByID(int id)
        {
            return this.genericRepository.GetByID(id);
        }


        public override GlobalEnums.AccessLevel GetAccessLevel(int? organizationalUnitID)
        {
            return this.genericRepository.GetAccessLevel(this.UserID, this.nmvnTaskID, organizationalUnitID);
        }

        public virtual bool Approvable(TDto dto)
        {
            if (dto.EntryDate <= this.genericRepository.GetEditLockedDate(this.UserID, this.nmvnTaskID)) return false;
            if (this.GetAccessLevel(dto.OrganizationalUnitID) != GlobalEnums.AccessLevel.Editable) return false;

            return this.genericRepository.GetApprovable(dto.GetID());
        }

        public virtual bool Editable(TDto dto)
        {
            if (!this.Approvable(dto)) return false;
            return this.genericRepository.GetEditable(dto.GetID());
        }

        public virtual bool Deletable(TDto dto)
        {
            if (!this.Editable(dto)) return false;
            return this.genericRepository.GetDeletable(dto.GetID());
        }


        protected virtual bool TryValidateModel(TDto dto)
        {
            StringBuilder invalidMessage = new StringBuilder();

            if (dto.EntryDate < new DateTime(2015, 7, 1) || dto.EntryDate > DateTime.Today.AddDays(2)) invalidMessage.Append(" Ngày không hợp lệ;");

            if (invalidMessage.ToString().Length > 0) throw new System.ArgumentException("Lỗi dữ liệu", invalidMessage.ToString());

            return true;
        }


        public virtual bool Save(TDto dto)
        {
            return this.Save(dto, false);
        }

        /// <summary>
        /// This is a protected method, to be accessible ONLY within its class and by derived class instances
        /// To use this, just call it from the Derived Classes
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="useExistingTransaction"></param>
        /// <returns></returns>
        protected virtual bool Save(TDto dto, bool useExistingTransaction)
        {
            TEntity entity;
            if (useExistingTransaction)
                entity = this.SaveThis(dto);
            else
                using (var dbContextTransaction = this.genericRepository.BeginTransaction())
                {
                    try
                    {
                        entity = this.SaveThis(dto);

                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }

            dto.SetID(entity.GetID());
            return true;
        }

        public virtual bool Delete(int id)
        {
            if (id <= 0) return false;

            using (var dbContextTransaction = this.genericRepository.BeginTransaction())
            {
                try
                {
                    TEntity entity = this.genericRepository.GetByID(id);
                    TDto dto = Mapper.Map<TDto>(entity);

                    if (!this.TryValidateModel(dto)) throw new System.ArgumentException("Lỗi xóa dữ liệu", "Dữ liệu này không hợp lệ.");
                    if (!this.Deletable(dto)) throw new System.ArgumentException("Lỗi xóa dữ liệu", "Dữ liệu này không thể xóa được.");

                    this.DeleteMe(dto, entity);

                    this.genericRepository.SaveChanges();

                    this.PostSaveValidate(entity);

                    dbContextTransaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public virtual void PreSaveRoutines(TDto dto)
        {
            if (dto.PreparedPersonID <= 0) throw new System.ArgumentException("Lỗi lưu dữ liệu", "Vui lòng chọn người lập.");

            OrganizationalUnitUser organizationalUnitUser = this.genericRepository.GetEntity<OrganizationalUnitUser>(w => w.UserID == dto.PreparedPersonID && !w.InActive, i => i.OrganizationalUnit);
            if (organizationalUnitUser != null)
            {
                dto.UserID = this.UserID;
                dto.OrganizationalUnitID = organizationalUnitUser.OrganizationalUnitID;
                dto.LocationID = organizationalUnitUser.OrganizationalUnit.LocationID;
            }
            else throw new System.ArgumentException("Lỗi lưu dữ liệu", "Vui lòng chọn người lập.");
        }

        private TEntity SaveThis(TDto dto)
        {
            this.PreSaveRoutines(dto);

            if (!this.TryValidateModel(dto)) throw new System.ArgumentException("Lỗi lưu dữ liệu", "Dữ liệu không hợp lệ.");
            if (!this.Editable(dto)) throw new System.ArgumentException("Lỗi lưu dữ liệu", "Bạn không có quyền lưu lại dữ liệu này.");

            dto.PerformPresaveRule();

            TEntity entity = this.SaveMe(dto);

            this.PostSaveValidate(entity);

            return entity;
        }

        protected virtual TEntity SaveMe(TDto dto)
        {
            TEntity entity = this.SaveMaster(dto);

            if (this.genericRepository.IsDirty())
                entity.EditedDate = DateTime.Now;

            this.genericRepository.SaveChanges();

            this.SaveRelative(entity, SaveRelativeOption.Update);

            return entity;
        }

        protected virtual TEntity SaveMaster(TDto dto)
        {
            TEntity entity;

            if (dto.GetID() > 0) //Edit existing Domain Model
            {
                entity = this.genericRepository.GetByID(dto.GetID());
                if (entity == null) throw new System.ArgumentException("", "Không tìm thấy dữ liệu. Dữ liệu cần điều chỉnh có thể đã bị xóa.");

                if (this.GetAccessLevel(entity.OrganizationalUnitID) != GlobalEnums.AccessLevel.Editable) throw new System.ArgumentException("", "Lưu ý: Bạn không có quyền điều chỉnh dữ liệu của người khác.");

                this.SaveRelative(entity, SaveRelativeOption.Undo);
            }
            else//New Domain Model
            {
                entity = new TEntity();
                entity.CreatedDate = DateTime.Now;

                this.genericRepository.Add(entity);
            }

            //Convert from DTOModel to Domain Model
            Mapper.Map<TPrimitiveDto, TEntity>((TPrimitiveDto)dto, entity);

            return entity;
        }

        protected virtual void DeleteMe(TDto dto, TEntity entity)
        {
            this.DeleteMaster(dto, entity);
            this.genericRepository.Remove(entity);
        }

        protected virtual void DeleteMaster(TDto dto, TEntity entity)
        {
            this.SaveRelative(entity, SaveRelativeOption.Undo);
        }

        protected virtual void PostSaveValidate(TEntity entity)
        {
            string foundInvalid = this.genericRepository.GetExisting(entity.GetID(), this.functionNamePostSaveValidate);
            if (foundInvalid != null)
                throw new Exception("Vui lòng kiểm tra: " + foundInvalid);
        }

        protected virtual void SaveRelative(TEntity entity, SaveRelativeOption saveRelativeOption)
        {
            if (this.functionNameSaveRelative != null && this.functionNameSaveRelative != "")
            {
                ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("EntityID", entity.GetID()), new ObjectParameter("SaveRelativeOption", (int)saveRelativeOption) };
                this.genericRepository.ExecuteFunction(this.functionNameSaveRelative, parameters);
            }
        }

    }
}
