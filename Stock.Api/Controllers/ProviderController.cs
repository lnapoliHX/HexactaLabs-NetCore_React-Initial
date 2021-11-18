using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));            
        }


        /// <summary>
        /// Get all providers.
        /// </summary>
        /// <returns>List of <see cref="ProviderDTO"/></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            logger.LogInformation("Mensaje Log de Get");
            try
            {
                var provider = service.GetAll();
                if (provider == null)
                {
                    return Ok(new { statusCode = "404", result = "No hay datos en Proveedores" });
                }
                return mapper.Map<IEnumerable<ProviderDTO>>(provider).ToList();
            }
            catch (Exception ex)
            {                
                return BadRequest(new { statusCode = "400", errorMessage = ex.Message });
            }        
        }

        /// <summary>
        /// Gets a provider by id.
        /// </summary>
        /// <param name="id">Provider id to return.</param>
        /// <returns>A <see cref="ProviderDTO"/></returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {            
            try
            {
                var provider = mapper.Map<ProviderDTO>(service.Get(id));
                if (provider == null) 
                {
                    return Ok(new { statusCode = "404", result = "El Proveedor no fué encontrado" });
                }
                return Ok(provider);
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = "400", errorMessage = ex.Message });
            }             

        }

        /// <summary>
        /// Add a provider
        /// </summary>
        /// <param name="value">Provider information.</param>
        [HttpPost]
        public Provider Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);
            var provider = service.Create(mapper.Map<Provider>(value));
            return mapper.Map<Provider>(provider);
        }

        /// <summary>
        /// Updates a provider.
        /// </summary>
        /// <param name="id">Provider id to edit.</param>
        /// <param name="value">Prodvider information.</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProviderDTO value)
        {            
            TryValidateModel(value);
            try
            {
                var provider = service.Get(id);
                if (provider == null) 
                {
                    return Ok(new { statusCode = "404", result = "El Proveedor no fué encontrado" });
                }
                mapper.Map<ProviderDTO, Provider>(value, provider);
                service.Update(provider);
                return Ok(new { statusCode = "200", result = "Proveedor modificado exitosamente" });
            }
            catch (Exception ex)
            {                
                return BadRequest(new { statusCode = "400", result = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a provider
        /// </summary>
        /// <param name="id">Provider id to delete.</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var provider = service.Get(id);
                if (provider is null)
                    return Ok(new { statusCode = "404", result = "El Proveedor no fué encontrado" });

                service.Delete(provider);
                return Ok(new { statusCode = "200", result = "Proveedor eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = "400", result = ex.Message });
            }
        }

        /// <summary>
        /// Search by Name, Phone, Email
        /// </summary>
        /// <param name="model">Provider information.</param>        
        /// <returns>A <see cref="ProviderSearchDTO"/></returns>
        [HttpGet]
        [Route("search")]
        public ActionResult Search([FromQuery] ProviderSearchDTO model) 
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            // Search by Name
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            // Search by Phone
            if (!string.IsNullOrWhiteSpace(model.Phone))
            {
                filter = filter.AndOrCustom(
                    x => x.Phone.ToUpper().Contains(model.Phone.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            // Search by Email
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                filter = filter.AndOrCustom(
                    x => x.Email.ToUpper().Contains(model.Email.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }


            var provider = service.Search(filter);
            if(provider.Count()==0)
                return Ok(new { statusCode = "404", result = "No se encontró información del Proveedor" });

            return Ok(provider);
        }
    }
}
