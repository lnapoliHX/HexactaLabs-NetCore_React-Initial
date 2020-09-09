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
        /// Permite crear un nuevo Store
        /// </summary>
        /// <param name="value">Un Store</param>
        [HttpPost]
        public ActionResult Post([FromBody] StoreDTO value)
        {
            TryValidateModel(value);

            try
            {
                var store = this.mapper.Map<Store>(value);
                this.service.Create(store);
                value.Id = store.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch
            {
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        /// <summary>
        /// Permite recuperar todos los Stores
        /// </summary>
        /// <returns>Una colección de Stores</returns>
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
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Permite recuperar un Store mediante un identificador
        /// </summary>
        /// <param name="id">Identificador del Store a recuperar</param>
        /// <returns>Un Store</returns>
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
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Permite editar un Store
        /// </summary>
        /// <param name="id">Identificador del Store a editar</param>
        /// <param name="value">Un Store con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] StoreDTO value)
        {
            var store = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<StoreDTO, Store>(value, store);
            this.service.Update(store);
        }

        /// <summary>
        /// Permite borrar un Store
        /// </summary>
        /// <param name="id">Identificador del Store a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var store = this.service.Get(id);

            this.service.Delete(store);
            return Ok(new { Success = true, Message = "", data = id });
        }

        /// <summary>
        /// Permite recuperar Store aplicando filtros
        /// </summary>
        /// <returns>Una colección de Store</returns>
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
            return Ok(stores);
        }
    }
}