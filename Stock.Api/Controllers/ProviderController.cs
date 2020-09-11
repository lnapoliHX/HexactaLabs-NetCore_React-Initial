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
        private ProviderService service;
        private readonly IMapper mapper;
        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        /// <summary>
        /// Permite recuperar todas las instancias
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                return this.mapper.Map<ProviderDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Permite crear un provider
        /// </summary>
        /// <param name ="value">Una Instancia</param> 
        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value){
            TryValidateModel(value);
            try{
                var provider = this.mapper.Map<Provider>(value);
                this.service.Create(provider);
                value.Id = provider.Id;
                return Ok(new {Success = true, Message = "", data = value});
            }
            catch (Exception e){ 
                return Ok(new {Success = false, Message = e.Message});
            }
        }
        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProviderDTO value)
        {
            var provider = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProviderDTO,Provider>(value,provider);
            this.service.Update(provider);
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
            return Ok(new{ Success = true, Message = "", data = id});
        }
        /// <summary>
        /// Permite recuperar todas las instancias que cumplan con los criterios de busqueda
        /// </summary>
        /// <param  name="model"> Criterio de Busqueda </param>
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

            var providers = this.service.Search(filter);
            return Ok(providers);
        }
    }
}