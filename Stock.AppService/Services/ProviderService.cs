using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System.Text.RegularExpressions;

namespace Stock.AppService.Services
{
    public class ProviderService : BaseService<Provider>
    {
        public ProviderService(IRepository<Provider> repository) : base(repository) {}

        public new Provider Create(Provider entity)
        {
			if (!this.NombreUnico(entity.Name)) { throw new System.Exception("The name is already in use"); }
			if (!this.TelefonoValido(entity.Phone)) { throw new System.Exception("Phone number has an invalid format"); }
			if (!this.CorreoValido(entity.Email)) { throw new System.Exception("E-mail has an invalid format"); }
			return base.Create(entity);
        }
		
        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return false; }
            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }
		
		private bool TelefonoValido(string phoneNumber)
		{
			Regex rgx = new Regex(@"^(\(?)\d{3}(\)?)(\-?)\d+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches(phoneNumber);
			if (matches.Count == 0) { return false; }
			else return true;
		}

		private bool CorreoValido(string email)
		{
			Regex rgx = new Regex(@"^([a-z]{1})([a-z0-9_.]+)\@{1}([a-z0-9_]+)\.([a-z]{3})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches(email);
			if (matches.Count == 0) { return false; }
			else return true;
		}
		
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        { return this.Repository.List(filter); }
    }
}