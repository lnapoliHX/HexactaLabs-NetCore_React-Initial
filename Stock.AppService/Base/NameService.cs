using Stock.Model.Base;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.AppService.Base
{
    public abstract class NameService<TNameEntity> : BaseService<TNameEntity> where TNameEntity : class, INameEntity, IEntity
    {
        protected NameService(IRepository<TNameEntity> repository) : base(repository)
        {

        }

        public bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

    }
}
