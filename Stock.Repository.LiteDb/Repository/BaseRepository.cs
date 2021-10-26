using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiteDB;
using Stock.Repository.LiteDb.Interface;
using Stock.Model.Base;
using Stock.Repository.LiteDb.Configuration;

namespace Stock.Repository.LiteDb.Repository
{
    /// <inheritdoc cref="IRepository{T}"/>
    public class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly IDbContext context;
        protected readonly ILiteCollection<T> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public BaseRepository(IDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            collection = GetCollection();
        }

        /// <inheritdoc />
        public T Add(T entity)
        {   
            entity.Id = Guid.NewGuid().ToString();
            collection.Insert(entity);
            return entity;
        }

        /// <inheritdoc />
        public void Delete(T entity)
        {
            collection.Delete(entity.Id);
        }

        /// <inheritdoc />
        public T GetById(string id)
        {
            var item = collection.FindOne(x => x.Id == id);
            return item;
        }

        /// <inheritdoc />
        public IReadOnlyList<T> List(Expression<Func<T, bool>> filter = null)
        {
            return Find(filter).ToList();
        }

        /// <inheritdoc />
        public IReadOnlyList<T> ListLimit(Expression<Func<T, bool>> filter = null, int size = 15)
        {
            return Find(filter).Take(size).ToList();
        }

        /// <inheritdoc />
        public void Update(T entity)
        {
            collection.Update(entity);
        }

        /// <summary>
        /// Gets a collection.
        /// </summary>
        /// <returns></returns>
        private ILiteCollection<T> GetCollection()
        {
            var collectionName = DocumentCollectionMapping.GetCollectionName<T>();
            var client = context.Database;

            return client.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Method to search by a specific filter.
        /// </summary>
        /// <param name="filter">Filter to apply.</param>
        /// <returns></returns>
        private IEnumerable<T> Find(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? collection.FindAll() : collection.Find(filter);
        }
    }
}