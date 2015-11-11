﻿using System;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Data.Entity;

using MVCBase.Enums;
using MVCModel.Models;
using MVCCore.Repositories;





using MVCModel; //for Loading (09/07/2015) - let review and optimize Loading laster





namespace MVCData.Repositories
{
    public class GenericRepository<TEntity> : BaseRepository, IGenericRepository<TEntity>
        where TEntity : class, IAccessControlAttribute //IAccessControlAttribute: for Loading (09/07/2015) - let review and optimize Loading laster
    {
        private DbSet<TEntity> modelDbSet = null;

        private readonly string functionNameEditable;
        private readonly string functionNameDeletable;
        private readonly string functionNameApprovable;


        public GenericRepository(TotalBikePortalsEntities totalBikePortalsEntities)
            : this(totalBikePortalsEntities, null) { }

        public GenericRepository(TotalBikePortalsEntities totalBikePortalsEntities, string functionNameEditable)
            : this(totalBikePortalsEntities, functionNameEditable, null) { }

        public GenericRepository(TotalBikePortalsEntities totalBikePortalsEntities, string functionNameEditable, string functionNameDeletable)
            : this(totalBikePortalsEntities, functionNameEditable, functionNameDeletable, null) { }

        public GenericRepository(TotalBikePortalsEntities totalBikePortalsEntities, string functionNameEditable, string functionNameDeletable, string functionNameApprovable)
            : base(totalBikePortalsEntities)
        {
            modelDbSet = this.TotalBikePortalsEntities.Set<TEntity>();

            this.functionNameEditable = functionNameEditable;
            this.functionNameDeletable = functionNameDeletable;
            this.functionNameApprovable = functionNameApprovable;
        }



        public DbContextTransaction BeginTransaction()
        {
            return this.TotalBikePortalsEntities.Database.BeginTransaction();
        }








        public virtual IQueryable<TEntity> Loading(string aspUserID, GlobalEnums.NmvnTaskID nmvnTaskID)//for Loading (09/07/2015) - let review and optimize Loading laster
        {
            int userID = this.TotalBikePortalsEntities.AspNetUsers.Where(w => w.Id == aspUserID).FirstOrDefault().UserID;
            return this.modelDbSet.Where(w => this.TotalBikePortalsEntities.AccessControls.Where(acl => acl.UserID == userID && acl.NMVNTaskID == (int)nmvnTaskID && acl.AccessLevel > 0).Select(s => s.OrganizationalUnitID).Contains(w.OrganizationalUnitID));
        }







        public IQueryable<TEntity> GetAll()
        {
            return this.modelDbSet;
        }

        public TEntity GetByID(int id)
        {
            return this.modelDbSet.Find(id);
        }



        public TEntity GetEntity(params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntity<TEntity>(includes);
        }
        public TEntity GetEntity(bool proxyCreationEnabled, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntity<TEntity>(proxyCreationEnabled, includes);
        }
        public TEntity GetEntity(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntity<TEntity>(predicate, includes);
        }
        public TEntity GetEntity(bool proxyCreationEnabled, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntity<TEntity>(proxyCreationEnabled, predicate, includes);
        }




        public ICollection<TEntity> GetEntities(params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntities<TEntity>(includes);
        }
        public ICollection<TEntity> GetEntities(bool proxyCreationEnabled, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntities<TEntity>(proxyCreationEnabled, includes);
        }
        public ICollection<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntities<TEntity>(predicate, includes);
        }
        public ICollection<TEntity> GetEntities(bool proxyCreationEnabled, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetEntities<TEntity>(proxyCreationEnabled, predicate, includes);
        }



        public DateTime GetEditLockedDate(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID)
        {
            return DateTime.Now.AddYears(-1);
        }


        public GlobalEnums.AccessLevel GetAccessLevel(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID, int? organizationalUnitID)
        {
            if (userID == null || userID == 0 || (int)nmvnTaskID == 0) return GlobalEnums.AccessLevel.Deny;

            int? accessLevel = this.TotalBikePortalsEntities.GetAccessLevel(userID, (int)nmvnTaskID, organizationalUnitID).Single();
            return accessLevel == null || accessLevel == (int)GlobalEnums.AccessLevel.Deny ? GlobalEnums.AccessLevel.Deny : (accessLevel == (int)GlobalEnums.AccessLevel.Readable ? GlobalEnums.AccessLevel.Readable : (accessLevel == (int)GlobalEnums.AccessLevel.Editable ? GlobalEnums.AccessLevel.Editable : GlobalEnums.AccessLevel.Deny));
        }


        public bool GetApprovable(int id)
        {
            return !this.CheckExisting(id, this.functionNameApprovable);
        }

        public bool GetEditable(int id)
        {
            return !this.CheckExisting(id, this.functionNameEditable);
        }

        public bool GetDeletable(int id)
        {
            return !this.CheckExisting(id, this.functionNameDeletable);
        }



        public bool CheckExisting(int id, string functionName)
        {
            return this.GetExisting(id, functionName) != null;
        }

        public string GetExisting(int id, string functionName)
        {
            if (id <= 0 || functionName == null || functionName == "")
                return null;
            else
            {
                ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("EntityID", id) };
                var foundEntityID = base.ExecuteFunction<string>(functionName, parameters);

                return foundEntityID.FirstOrDefault();
            }
        }


        public TEntity Add(TEntity entity)
        {
            return this.modelDbSet.Add(entity);
        }

        public TEntity Remove(TEntity entity)
        {
            return this.modelDbSet.Remove(entity);
        }

        public int SaveChanges()
        {
            return this.TotalBikePortalsEntities.SaveChanges();
        }


    }
}
