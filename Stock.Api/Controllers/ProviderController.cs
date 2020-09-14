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
    public class ProviderController: ControllerBase
    {
         private readonly ProviderService service;
        private readonly IMapper mapper;
        
        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>
        /// Permite crear un nuevo provider
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public Provider Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);
            var provider = this.service.Create(this.mapper.Map<Provider>(value));
            return this.mapper.Map<Provider>(provider);
        }

        /// <summary>
        /// Permite editar un provider
        /// </summary>
        /// <param name="id">Identificador del provider a editar</param>
        /// <param name="value">Un provider con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProviderDTO value)
        {
            var provider = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProviderDTO, Provider>(value, provider);
            this.service.Update(provider);
        }

        /// <summary>
        /// Permite borrar un provider
        /// </summary>
        /// <param name="id">Identificador del provider a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = this.service.Get(id);

             Expression<Func<Product, bool>> filter = x => x.Provider.Id.Equals(id);
            
            this.service.Delete(provider);
            return Ok();
        }

             /// <summary>
        /// Permite recuperar todos los provider
        /// </summary>
        /// <returns>Una colección de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProviderDTO>>(this.service.GetAll()).ToList();
        }

        /// <summary>
        /// Permite recuperar un provider mediante un identificador
        /// </summary>
        /// <param name="id">Identificador del provider a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            return this.mapper.Map<ProviderDTO>(this.service.Get(id));
        }

        
    }
}