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
        /// Permite agregar una nueva Tienda al repositorio
        /// </summary>
        /// <param name="value">Propiedades de la Tienda</param>
        /// <returns>Exito o Error y la Tienda que se intentó crear</returns>
        [HttpPost]
        public ActionResult Post([FromBody] StoreDTO value)
        {
            TryValidateModel(value);

            try
            {
                var store = this.mapper.Map<Store>(value);
                this.service.Create(store);
                value.Id = store.Id;
                return Ok(new { Success = true, Message = "Store Created!", Store = value });
            }
            catch
            {
                return Ok(new { Success = false, 
                                Message = "The name is already in use!!!", Store = value });
            }
        }

        /// <summary>
        /// Permite recuperar todas las Tiendas existentes en el repositorio
        /// </summary>
        /// <returns>Una colección de Tiendas o un código en caso de error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<StoreDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
//                return this.mapper.Map<IEnumerable<StoreDTO>>(result).ToList();
                return Ok(new {Success = true, Message = "List of all Stores", 
                                Stores = this.mapper.Map<IEnumerable<StoreDTO>>(result).ToList()} );
            }
            catch (Exception)
            {
                //return StatusCode(500);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite recuperar las propiedades de una Tienda mediante su Id
        /// </summary>
        /// <param name="id">Identificador de la Tienda a recuperar</param>
        /// <returns>Una instancia de Tienda o un código en caso de error</returns>
        [HttpGet("{id}")]
        public ActionResult<StoreDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
//                return this.mapper.Map<StoreDTO>(result);
                return Ok(new { Success = true, Message = "Store Obtained!", 
                        Store = this.mapper.Map<StoreDTO>(result) });
            }
            catch (Exception)
            {
//                return StatusCode(500);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite actualizar las propiedades de una Tienda
        /// </summary>
        /// <param name="id">Identificador de la Tienda a actualizar</param>
        /// <param name="value">Propiedades de la Tienda</param>
        /// <returns>Exito o Error y la Tienda que se intentó actualizar</returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] StoreDTO value)
        {
            TryValidateModel(value);

            var store = this.service.Get(id);
            try
            {
                this.mapper.Map<StoreDTO, Store>(value, store);
                this.service.Update(store);
                return Ok(new { Success = true, Message = "Store Updated!", Store = store });
            }
            catch
            {
                return Ok(new { Success = false, 
                                Message = "The name is already in use!!!", Store = store });
            }
        }

        /// <summary>
        /// Permite borrar una Tienda
        /// </summary>
        /// <param name="id">Identificador de la Tienda a borrar</param>
        /// <returns>Exito y el Id de la Tienda eliminada o un codigo en caso de error</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var store = this.service.Get(id);

            try
            {          
                this.service.Delete(store);
                return Ok(new { Success = true, Message = "Store Deleted!", data = id });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            //this.service.Delete(store);
            //return Ok(new { Success = true, Message = "", data = id });
        }

        /// <summary>
        /// Permite realizar la búsqueda de Tiendas
        /// </summary>
        /// <param name="model">Objeto que contiene los parametros de Busquesda</param>
        /// <returns>Lista de las Tiendas filtradas</returns>
        [HttpPost("search")]
        public ActionResult Search([FromBody] StoreSearchDTO model)
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
//            return Ok(stores);
            return Ok(new {Success = true, Message = "List of all Stores", 
                            Stores = stores} );
        }
    }
}