using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Stock.Api.Extensions;

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
        public ActionResult Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);
             try
            {
                var provider = this.mapper.Map<Provider>(value);
                this.service.Create(provider);
                value.Id = provider.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch(Exception)
            {
                return Ok(new { Success = false, Message = "The email is already in use" });
            }
        }


        [HttpPost("search")]
        public ActionResult getOne([FromBody] ProviderDTO model)
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                filter = filter.AndOrCustom(
                    x => x.Email.ToUpper().Contains(model.Email.ToUpper()));
            }

            var provider = this.service.getOne(filter);
            return Ok(provider);
            
        }
        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProviderDTO value)
        {
            if(id != null && !string.IsNullOrWhiteSpace(id)){
                var provider = this.service.Get(id);
                TryValidateModel(value);
                this.mapper.Map<ProviderDTO, Provider>(value, provider);
                this.service.Update(provider);
            }
        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if(!string.IsNullOrWhiteSpace(id)){
                var provider = this.service.Get(id);

                Expression<Func<Product, bool>> filter = x => x.Provider.Id.Equals(id);
                    
                this.service.Delete(provider);
                return Ok(new { Success = true, Message = "The provider was delete" });

            } else {
                return Ok(new { Success = false, Message = "The id doesn't exist" });
            }
        }
    }
}