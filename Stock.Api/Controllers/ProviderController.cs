using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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

        /// <summary> Permite crear una instancia </summary>
        /// <param name="value"> Instancia a crear </param>
        /// <returns> Instancia creada </returns>
        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);

            try
            {
                var provider = this.mapper.Map<Provider>(value);
                this.service.Create(provider);
                value.Id = provider.Id;
                return Ok(new { Success = true, Message = "Provider successfully created", data = value });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary> Permite obtener todas las instancias </summary>
        /// <returns> Una colección de instancias </returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return Ok(this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary> Permite obtener una instancia por identificador </summary>
        /// <param name="id"> Identificador de la instancia a borrar </param>
        /// <returns> Una instancia </returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                return Ok(this.mapper.Map<ProviderDTO>(result));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary> Permite actualizar una instancia por identificador </summary>
        /// <param name="id"> Identificador de la instancia a actualizar </param>
        /// <returns> Una instancia </returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProviderDTO value)
        {
            TryValidateModel(value);

            try
            {
                var provider = this.service.Get(id);                
                this.mapper.Map<ProviderDTO, Provider>(value, provider);
                this.service.Update(provider);
                return Ok(new { Success = true, Message = "Provider successfully updated", data = value });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary> Permite borrar una instancia </summary>
        /// <param name="id"> Identificador de la instancia a borrar </param>
        /// <returns> Una instancia </returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try 
            {
                this.service.Delete(this.service.Get(id));
                return Ok(new { Success = true, Message = "Provider successfully deleted", data = id });            
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary> Permite obtener instancias en base a filtros de busqueda </summary>
        /// <param name="model"> Instancia de ProviderSearchDTO con filtros de busqueda </param>
        /// <returns> Una colección de instancias </returns>
        [HttpPost("search")]
        public ActionResult Search([FromBody] ProviderSearchDTO model)
        {
            try
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

                var stores = this.service.Search(filter);
                return Ok(stores);
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
    }
}