using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
        private ProviderService service;
        private readonly IMapper mapper;

        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>
        /// Permite agregar un nuevo Proveedor al repositorio
        /// </summary>
        /// <param name="value">Propiedades del Proveedor</param>
        /// <returns>Exito o Error y el Proveedor que se intentó crear</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);

            try
            {
                var provider = this.mapper.Map<Provider>(value);
                this.service.Create(provider);
                value.Id = provider.Id;
                return Ok(new { Success = true, Message = "Provider Created!", Provider = value });
            }
            catch
            {
                return Ok(new { Success = false, 
                                Message = "The name is already in use!!!", Provider = value });
            }
        }

        /// <summary>
        /// Permite recuperar todos los Proveedores existentes en el repositorio
        /// </summary>
        /// <returns>Una colección de Proveedores o un código en caso de error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                //return this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
                return Ok(new {Success = true, Message = "List of all Providers", 
                                Providers = this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList()} );
            }
            catch (Exception)
            {
                //return StatusCode(500);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite recuperar las propiedades de un Proveedor mediante su Id
        /// </summary>
        /// <param name="id">Identificador del Proveedor a recuperar</param>
        /// <returns>Una instancia de Proveedor o un código en caso de error</returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                //return this.mapper.Map<ProviderDTO>(result);
                return Ok(new { Success = true, Message = "Provider Obtained!", 
                        Provider = this.mapper.Map<ProviderDTO>(result) });

            }
            catch (Exception)
            {
                //return StatusCode(500);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite actualizar las propiedades de un Proveedor
        /// </summary>
        /// <param name="id">Identificador del Proveedor a actualizar</param>
        /// <param name="value">Propiedades del Proveedor</param>
        /// <returns>Exito o Error y el Proveedor que se intentó actualizar</returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProviderDTO value)
        {
            TryValidateModel(value);

            var provider = this.service.Get(id);
            try
            {
                this.mapper.Map<ProviderDTO, Provider>(value, provider);
                this.service.Update(provider);
                //return Ok(new { Success = true, Message = "", data = id });
                return Ok(new { Success = true, Message = "Provider Updated!", Provider = provider });
            }
            catch
            {
                return Ok(new { Success = false, 
                                Message = "The name is already in use!!!", Provider = provider });
            }
            
        }

        /// <summary>
        /// Permite borrar un Proveedor
        /// </summary>
        /// <param name="id">Identificador del Proveedor a borrar</param>
        /// <returns>Exito y el Id del Proveedor eliminado o un código en caso de error</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = this.service.Get(id);

            try
            {          
                this.service.Delete(provider);
                return Ok(new { Success = true, Message = "Provider Deleted!", data = id });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite realizar la búsqueda de Proveedores
        /// </summary>
        /// <param name="model">Objeto que contiene los parametros de Busquesda</param>
        /// <returns>Lista de los Proveedores filtrados</returns>
        [HttpPost("search")]
        public ActionResult Search([FromBody] ProviderSearchDTO model)
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                filter = filter.AndOrCustom(
                    x => x.Email.ToLower().Contains(model.Email.ToLower()),
                    model.Condition.Equals(ActionDto.AND));
            }

            var providers = this.service.Search(filter);
            //return Ok(providers);
            return Ok(new {Success = true, Message = "List of filtered Providers", 
                            Providers = providers} );
        }
    }
}