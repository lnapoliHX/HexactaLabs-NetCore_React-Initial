using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;


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
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
           //  var provider = this.service.Get(id);

            try
            {
                var provider = this.service.Get(id);
                var providerDTO = this.mapper.Map<ProviderDTO>(provider);

                return Ok(new { Success = true, Message = "", data = providerDTO });  
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = ex.Message, data = "" });
            }

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
                var provider = this.service.Create(this.mapper.Map<Provider>(value));
                value.Id = provider.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = ex.Message ,data = "" });
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
            this.mapper.Map<ProviderDTO, Provider>(value, provider);
            this.service.Update(provider);

        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var provider = this.service.Delete(id);
                return Ok(new { Success = true, Message = "", data = id });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = ex.Message, data = "" });
            }

        }
    }
}
