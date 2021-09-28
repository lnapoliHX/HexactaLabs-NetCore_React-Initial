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
    public class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly IDbContext context;
        protected readonly ILiteCollection<T> collection;

        public BaseRepository(IDbContext context)
        {
            this.context = context;
            collection = GetCollection();
        }

        public T Add(T entity)
        {   
            entity.Id = Guid.NewGuid().ToString();
            collection.Insert(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            collection.Delete(entity.Id);
        }

        public T GetById(string id)
        {
            var item = collection.Find(x => x.Id == id).ToList().FirstOrDefault();
            return item;
        }

        public IReadOnlyList<T> List(Expression<Func<T, bool>> filter = null)
        {
            return Find(filter).ToList();
        }

        public IReadOnlyList<T> ListLimit(Expression<Func<T, bool>> filter = null, int size = 15)
        {
            return Find(filter).Take(size).ToList();
        }

        public void Update(T entity)
        {
            collection.Update(entity);
        }

        private ILiteCollection<T> GetCollection()
        {
            var collectionName = DocumentCollectionMapping.GetCollectionName<T>();
            var client = context.Database;

            return client.GetCollection<T>(collectionName);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? collection.FindAll() : collection.Find(filter);
        }
    }
}