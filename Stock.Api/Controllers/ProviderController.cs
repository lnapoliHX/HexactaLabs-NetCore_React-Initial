using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
        /// <returns>Una colección de instancias de tipo Proveedor</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
            }
            catch (Exception) { return StatusCode(500); }
        }

        /// <summary>Permite recuperar una instancia de Proveedor utilizando un ID</summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Devuelve una instancia de proveedor acorde a su identificador</returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                return this.mapper.Map<ProviderDTO>(result);
            }
            catch (Exception) { return NoContent(); }
        }

        /// <summary>Permite crear una nueva instancia de tipo Proveedor</summary>
        /// <param name="value">Una instancia</param>
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
            catch (System.Exception err) { return Ok(new { Success = false, Message = err.Message.ToString() }); }
        }

        /// <summary>Permite editar una instancia de tipo Proveedor</summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProviderDTO value)
        {
            try
            {
                var vendor = this.service.Get(id);
                TryValidateModel(value);
                this.mapper.Map<ProviderDTO, Provider>(value, vendor);
                this.service.Update(vendor);
                return Ok(new { Success = true, Message = "Provider succesfully updated", data = value });
            }

            catch (System.Exception) { return NotFound(new { Success = false, Message = "Provider not found" }); }
        }

        /// <summary>Permite borrar una instancia de Proveedor</summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
			try
			{
				var vendor = this.service.Get(id);
				this.service.Delete(vendor);
				return Ok(new { Success = true, Message = "Provider succesfully deleted", data = id });
			}
            catch { return NotFound(new { Success = false, Message = "Provider was unable to be deleted" }); }
        }
    }
}