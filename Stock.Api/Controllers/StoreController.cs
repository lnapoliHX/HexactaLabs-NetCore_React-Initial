using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using Stock.Model.Exceptions;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private StoreService service;
        private readonly IMapper mapper;

        public StoreController(StoreService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>
        /// Permite crear una nueva instancia
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public ActionResult Post([FromBody] StoreDTO value)
        {
            TryValidateModel(value);

            try
            {
                var store = this.mapper.Map<Store>(value);
                this.service.Create(store);
                value.Id = store.Id;
                return Ok(new { Success = true, Message = "Created", data = value });
            }
            catch (NameAlreadyInUseException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        /// Permite recuperar todas las instancias
        /// </summary>
        /// <returns>Una colección de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<StoreDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return this.mapper.Map<IEnumerable<StoreDTO>>(result).ToList();
            }
            catch (Exception)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<StoreDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                return this.mapper.Map<StoreDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] StoreDTO value)
        {
            try
            {
                var store = this.service.Get(id);
                TryValidateModel(value);
                this.mapper.Map<StoreDTO, Store>(value, store);
                this.service.Update(store);
                return Ok(new { Success = true, Message = "Updated", data = value });
            }
            catch (NameAlreadyInUseException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }
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
                var store = this.service.Get(id);

                this.service.Delete(store);
                return Ok(new { Success = true, Message = "Deleted", data = id });
            }
            catch(Exception) 
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        /// Permite recuperar instancias en base a un filtro 
        /// </summary>
        /// <param name="model">Una instancia de StoreSearchDTO</param>
        [HttpPost("search")]
        public ActionResult Search([FromBody] StoreSearchDTO model)
        {
            try
            {
                Expression<Func<Store, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

                if (!string.IsNullOrWhiteSpace(model.Name))
                {
                    filter = filter.AndOrCustom(
                        x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                        model.Condition.Equals(ActionDto.AND));
                }

                if (!string.IsNullOrWhiteSpace(model.Address))
                {
                    filter = filter.AndOrCustom(
                        x => x.Address.ToUpper().Contains(model.Address.ToUpper()),
                        model.Condition.Equals(ActionDto.AND));
                }

                var stores = this.service.Search(filter);
                return Ok(stores);
            }
            catch(Exception) 
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }
        }
    }
}