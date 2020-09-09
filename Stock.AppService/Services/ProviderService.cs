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
        ///<summary>Constructor de la clase ProviderService</summary>
        ///<param name="repository">Repositorio</param>
        public ProviderService(IRepository<Provider> repository) : base(repository) {}

        ///<summary>Metodo que crea un nuevo servicio de tipo Provider</summary>
        ///<param name="entity">Entidad de tipo proveedor</param>
        ///<returns>Si la operacion fue correcta, retornara una nueva entidad de tipo Provider</returns>
        public new Provider Create(Provider entity)
        {
            if (!this.NombreUnico(entity.Name)) throw new System.Exception("The name is already in use");
            if (!this.TelefonoValido(entity.Phone)) throw new System.Exception("Phone number has an invalid format");
            if (!this.CorreoValido(entity.Email)) throw new System.Exception("E-mail has an invalid format");
			return base.Create(entity);
        }
		
        ///<summary>Metodo que identifica si el nombre provisto no fue anteriormente utilizado</summary>
        ///<param name="name">Nombre del cual se desea chequear duplicidad</param>
        ///<returns>Si el nombre fue previamente declarado o se proporciono un nombre en blanco, devolvera False; de lo contrario devuelve True</returns>
        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return false; }
            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }
		
        ///<summary>Metodo que valida la estructura de un telefono mediante el uso de expresiones regulares</summary>
        ///<param name="phoneNumber">Numero telefonico a validar</param>
        ///<returns>Si el numero telefonico contiene letras o caracteres invalidos, devolvera False; de lo contrario devuelve True</returns>
		private bool TelefonoValido(string phoneNumber)
		{
			Regex rgx = new Regex(@"^(\(?)\d{3}(\)?)(\-?)\d+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches(phoneNumber);
			if (matches.Count == 0) return false;
			else return true;
		}

        ///<summary>Metodo que valida la estructura de un correo electronico mediante el uso de expresiones regulares</summary>
        ///<param name="phoneNumber">Correo electronico a validar</param>
        ///<returns>Si el correo electronico no inicia con una letra o caracteres invalidos, devolvera False; de lo contrario devuelve True</returns>
		private bool CorreoValido(string email)
		{
			Regex rgx = new Regex(@"^([a-z]{1})([a-z0-9_]+)\@{1}([a-z0-9_]+)\.([a-z]{3})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches(email);
			if (matches.Count == 0) return false;
			else return true;
		}
		
		///<summary>Metodo encargado de buscar una entidad de tipo Proveedor en base a un filtro</summary>
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        { return this.Repository.List(filter); }
    }
}