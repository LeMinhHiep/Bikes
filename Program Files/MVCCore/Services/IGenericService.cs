﻿using MVCBase.Enums;
using MVCModel;
using MVCDTO;


namespace MVCCore.Services
{
    public interface IGenericService<TEntity, TDto, TPrimitiveDto>: IBaseService

        where TEntity : class, IPrimitiveEntity, IBaseEntity, new()
        where TDto : class, TPrimitiveDto
        where TPrimitiveDto : BaseDTO, IPrimitiveEntity, IPrimitiveDTO, new()
    {
        TEntity GetByID(int id);

        bool Editable(TDto dto);
        bool Deletable(TDto dto);

        bool Save(TDto dto);
        bool Delete(int id);

        void PreSaveRoutines(TDto dto);
    }
}
