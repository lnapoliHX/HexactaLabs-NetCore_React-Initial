using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Model.Exceptions;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    public class StoreService : BaseService<Store>
    {
        public StoreService(IRepository<Store> repository) : base(repository)
        {

        }

        public new Store Create(Store entity)
        {
            if (this.NombreUnico(entity.Name))
            {
                return base.Create(entity);
            }

            throw new NameAlreadyInUseException(entity.Name);
        }
        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        public IEnumerable<Store> Search(Expression<Func<Store, bool>> filter)
        {
            return this.Repository.List(filter);
        }

        public new Store Update(Store entity)
        {

            if (this.NombreUnico(entity.Name, entity.Id))
            {
                return base.Update(entity);
            }

            throw new NameAlreadyInUseException(entity.Name);
        }

        private bool NombreUnico(string name, string id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return this.Repository.List((x =>
                x.Name.ToUpper().Equals(name.ToUpper()) &&
                !x.Id.Equals(id))).Count == 0;
        }
    }
}