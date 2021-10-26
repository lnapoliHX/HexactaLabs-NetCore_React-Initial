using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.Model.Base;

namespace Stock.Repository.LiteDb.Interface
{
    /// <summary>
    /// Base repository, defines CRUD operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// Gets and entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(string id);

        /// <summary>
        /// Returns a list of items by a specific filter.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <returns></returns>
        IReadOnlyList<T> List(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Returns a specific number of items by a specific filter.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <param name="size">Number of items to return, default 15 items.</param>
        /// <returns></returns>
        IReadOnlyList<T> ListLimit(Expression<Func<T, bool>> filter = null, int size = 15);

        /// <summary>
        /// Adds an entity.
        /// </summary>
        /// <param name="entity">Entity information.</param>
        /// <returns>Created entity.</returns>
        T Add(T entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">New entity information.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        void Delete(T entity);
    }
}