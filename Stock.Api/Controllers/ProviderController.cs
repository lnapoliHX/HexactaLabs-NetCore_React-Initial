using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService service;
        private readonly IMapper mapper;
        
        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>Permite recuperar todas las instancias del Proveedor</summary>
        /// <returns>Devuelve una colección de instancias de tipo Proveedor</returns>
		/// <exception cref="System.Exception">Arroja error de codigo 500 cuando la operacion no pudo ser llevada a cabo</exception>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
            }
            catch (System.Exception) { return StatusCode(500); }
        }

        /// <summary>Permite recuperar una instancia de Proveedor utilizando un ID</summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Devuelve una instancia de proveedor acorde a su identificador</returns>
		/// <exception cref="System.Exception">Arroja error cuando los criterios provistos no son suficientes para encontrar el proveedor</exception>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var vendor = this.service.Get(id);
				if (vendor == null) { throw new System.Exception("There is no provider found by given ID"); }
                return this.mapper.Map<ProviderDTO>(vendor);
            }
            catch (System.Exception err) { return NotFound(new { Success = false, Message = err.Message.ToString() }); }
        }

        /// <summary>Permite crear una nueva instancia de tipo Proveedor</summary>
        /// <param name="value">Instancia con los nuevos datos</param>
        /// <returns>Devuelve una notificacion de nueva instancia creada</returns>
        /// <remarks>En caso de no proporcionar un ID manualmente, el sistema lo calcula por si mismo</remarks>
		/// <exception cref="System.Exception">Arroja error cuando el telefono o correo electronicos son invalidos</exception>
        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);

            try
            {
                var vendor = this.mapper.Map<Provider>(value);
                this.service.Create(vendor);
                value.Id = vendor.Id;
                return Ok(new { Success = true, Message = "Provider succesfully created", data = value });
            }
            catch (System.Exception err) { return NotFound(new { Success = false, Message = err.Message.ToString() }); }
        }

        /// <summary>Permite editar una instancia de tipo Proveedor</summary>
        /// <param name="id">Identificador de la instancia a modificar</param>
        /// <param name="value">Instancia con los nuevos datos</param>
        /// <returns>Devuelve una notificacion de instancia modificada</returns>
		/// <exception cref="System.Exception">Arroja error cuando el identificador no es suficiente para encontrar el proveedor,
		/// o cuando el telefono o direccion de correo electronico es invalida</exception>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProviderDTO value)
        {
            try
            {
                var vendor = this.service.Get(id);
                TryValidateModel(value);
				if (vendor == null) { throw new System.Exception("There is no provider found by given ID"); }
                this.mapper.Map<ProviderDTO, Provider>(value, vendor);
                vendor.Id = id;
                this.service.Update(vendor);
                return Ok(new { Success = true, Message = "Provider succesfully updated", data = value });
            }

            catch (System.Exception err) { return NotFound(new { Success = false, Message = err.Message.ToString() }); }
        }

        /// <summary>Permite borrar una instancia de Proveedor</summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        /// <returns>Devuelve una notificacion de instancia eliminada</returns>
		/// <exception cref="System.Exception">Arroja error cuando el identificador no es suficiente para encontrar el proveedor</exception>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
			try
			{
				var vendor = this.service.Get(id);
				this.service.Delete(vendor);
				return Ok(new { Success = true, Message = "Provider succesfully deleted", data = id });
			}
            catch(System.Exception) { return NotFound(new { Success = false, Message = "Provider was unable to be deleted" }); }
        }

        /// <summary>Permite efectuar una busqueda personalizada de Proveedores</summary>
        /// <param name="model">Atributo de Proveedor sobre el cual efectuar la busqueda</param>
        /// <returns>Devuelve la instancia encontrada</returns>
		/// <exception cref="System.Exception">Arroja error cuando los criterios provistos no son suficientes para encontrar el proveedor</exception>
        [HttpPost("search")]
        public ActionResult Search([FromBody] ProviderSearchDTO model)
        {
			try
			{
				Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);
				
				//Busqueda por nombre
				if (!string.IsNullOrWhiteSpace(model.Name))
				{ filter = filter.AndOrCustom(x => x.Name.ToUpper().Contains(model.Name.ToUpper()), model.Condition.Equals(ActionDto.AND)); }
				
				//Busqueda por telefono
				if (!string.IsNullOrWhiteSpace(model.Phone))
				{ filter = filter.AndOrCustom(p => p.Phone.Contains(model.Phone), model.Condition.Equals(ActionDto.AND)); }

				//Busqueda por correo electronico
				if (!string.IsNullOrWhiteSpace(model.Email))
				{ filter = filter.AndOrCustom(p => p.Email.ToUpper().Contains(model.Email.ToUpper()), model.Condition.Equals(ActionDto.AND)); }
				
				var vendor = this.service.Search(filter);
				if (vendor == null) { throw new System.Exception("There is no provider found by given criteria"); }
				return Ok(vendor);
			}
			catch (System.Exception err) { return NotFound(new { Success = false, Message = err.Message.ToString() }); }
        }
    }
}