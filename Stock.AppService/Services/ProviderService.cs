using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Model.Exceptions;
using Stock.Repository.LiteDb.Interface;

public class ProviderService : BaseService<Provider>
{
    public ProviderService(IRepository<Provider> repository) : base(repository) { }

    public new Provider Create(Provider entity)
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

    public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
    {
        return this.Repository.List(filter);
    }

    public new Provider Update(Provider entity)
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