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
        protected readonly LiteCollection<T> collection;

        public BaseRepository(IDbContext context)
        {
            this.context = context;
            this.collection = this.GetCollection();
        }

        public T Add(T entity)
        {   
            entity.Id = Guid.NewGuid().ToString();
            this.collection.Insert(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            this.collection.Delete(x => x.Id == entity.Id);
        }

        public T GetById(string id)
        {
            var item = this.collection.Find(x => x.Id == id).ToList().FirstOrDefault();
            return item;
        }

        public IReadOnlyList<T> List(Expression<Func<T, bool>> filter = null)
        {
            return this.Find(filter).ToList();
        }

        public IReadOnlyList<T> ListLimit(Expression<Func<T, bool>> filter = null, int size = 15)
        {
            return this.Find(filter).Take(size).ToList();
        }

        public void Update(T entity)
        {
            this.collection.Update(entity);
        }

        private LiteCollection<T> GetCollection()
        {
            var collectionName = DocumentCollectionMapping.GetCollectionName<T>();
            var client = this.context.Database;

            return client.GetCollection<T>(collectionName);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return this.collection.FindAll();
            }
            else
            {
                return this.collection.Find(filter);
            }
        }
    }
}