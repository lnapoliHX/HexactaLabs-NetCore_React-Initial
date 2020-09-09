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
            /// Permite recuperar todas las instancias
            /// </summary>
            /// <returns>Una colección de instancias</returns>
            [HttpGet]

            public ActionResult<IEnumerable<ProviderDTO>> Get()
            {
                return this.mapper.Map<IEnumerable<ProviderDTO>>(this.service.GetAll()).ToList();
            }

            /// <summary>
            /// Permite crear una nueva instancia
            /// </summary>
            /// <param name="value">Una instancia</param>
            [HttpPost]
            public Provider Post([FromBody] ProviderDTO value)
            {
                TryValidateModel(value);
                var Provider = this.service.Create(this.mapper.Map<Provider>(value));
                return this.mapper.Map<Provider>(Provider);
            }

            /// <summary>
            /// Permite borrar una instancia
            /// </summary>
            /// <param name="id">Identificador de la instancia a borrar</param>
            [HttpDelete("{id}")]
            public ActionResult Delete(string id)
            {
            var Provider = this.service.Get(id);

             Expression<Func<Product, bool>> filter = x => x.Provider.Id.Equals(id);
            
            this.service.Delete(Provider);
            return Ok();
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
            this.mapper.Map<ProviderDTO, Provider>(value, provider);
            this.service.Update(provider);
        }

        /// Buscar proveedores por nombre
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