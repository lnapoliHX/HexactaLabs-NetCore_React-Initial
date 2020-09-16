using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using Stock.Model.Exceptions;

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
        /// Permite crear una instancia mediante un identificador
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);
            var provider = this.mapper.Map<Provider>(value);
            this.service.Create(provider);
            value.Id = provider.Id;
            return Ok(new { Success = true, Message = "Provider was created succesfully", data = value });         
        }

        /// <summary>
        /// Permite recuperar todas las instancias
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            var result = this.service.GetAll();
            return this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
        }

        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            var result = this.service.Get(id);
            return this.mapper.Map<ProviderDTO>(result);
        }
        
        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value"> Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProviderDTO value)
        {
            var provider = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProviderDTO, Provider>(value, provider);
            this.service.Update(provider);
            return Ok(new{success=true,message="The Provider was succesfully updated", data = value});
        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = this.service.Get(id);
            this.service.Delete(provider);
            return Ok(new { Success = true, Message = "The provider was succesfully deleted", data = id });
        }

        /// <summary>
        /// Permite buscar una instancia a partir del uso de filtros
        /// </summary>
        /// <param name="model">valores de la instancia de ProviderSearchDTO para aplicar filtros de búsqueda</param>
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
                    x => x.Email.ToUpper().Contains(model.Email.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            var provider = this.service.Search(filter);
            return Ok(provider);
        }
    }
}