using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.Model.Base;

namespace Stock.Repository.LiteDb.Interface
{
    public interface IRepository<T> where T : IEntity
    {
        T GetById(string id);
        IReadOnlyList<T> List(Expression<Func<T, bool>> filter = null);
        IReadOnlyList<T> ListLimit(Expression<Func<T, bool>> filter = null, int size = 15);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}