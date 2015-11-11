using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;


namespace MVCCore.Repositories
{
    public interface IBaseRepository
    {
        bool IsDirty();

        ICollection<TElement> ExecuteFunction<TElement>(string functionName, params ObjectParameter[] parameters);
        int ExecuteFunction(string functionName, params ObjectParameter[] parameters);



        T GetEntity<T>(params Expression<Func<T, object>>[] includes) where T : class;
        T GetEntity<T>(bool proxyCreationEnabled, params Expression<Func<T, object>>[] includes) where T : class;
        T GetEntity<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;
        T GetEntity<T>(bool proxyCreationEnabled, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;


        ICollection<T> GetEntities<T>(params Expression<Func<T, object>>[] includes) where T : class;
        ICollection<T> GetEntities<T>(bool proxyCreationEnabled, params Expression<Func<T, object>>[] includes) where T : class;
        ICollection<T> GetEntities<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;
        ICollection<T> GetEntities<T>(bool proxyCreationEnabled, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;

            

    }
}
