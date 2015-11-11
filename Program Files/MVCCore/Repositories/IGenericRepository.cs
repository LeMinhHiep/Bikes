﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Collections.Generic;

using MVCBase.Enums;

namespace MVCCore.Repositories
{
    public interface IGenericRepository<TEntity> : IBaseRepository
        where TEntity : class
    {
        DbContextTransaction BeginTransaction();






        IQueryable<TEntity> Loading(string aspUserID, GlobalEnums.NmvnTaskID nmvnTaskID);//for Loading (09/07/2015) - let review and optimize Loading laster






        IQueryable<TEntity> GetAll();
        TEntity GetByID(int id);



        TEntity GetEntity(params Expression<Func<TEntity, object>>[] includes);
        TEntity GetEntity(bool proxyCreationEnabled, params Expression<Func<TEntity, object>>[] includes);
        TEntity GetEntity(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        TEntity GetEntity(bool proxyCreationEnabled, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);


        ICollection<TEntity> GetEntities(params Expression<Func<TEntity, object>>[] includes);
        ICollection<TEntity> GetEntities(bool proxyCreationEnabled, params Expression<Func<TEntity, object>>[] includes);
        ICollection<TEntity> GetEntities(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        ICollection<TEntity> GetEntities(bool proxyCreationEnabled, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);



        DateTime GetEditLockedDate(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID);
        GlobalEnums.AccessLevel GetAccessLevel(int? userID, GlobalEnums.NmvnTaskID nmvnTaskID, int? organizationalUnitID);

        bool GetApprovable(int id);
        bool GetEditable(int id);
        bool GetDeletable(int id);
        bool CheckExisting(int id, string functionName);
        string GetExisting(int id, string functionName);


        TEntity Add(TEntity entity);
        TEntity Remove(TEntity entity);

        int SaveChanges();

    }
}
