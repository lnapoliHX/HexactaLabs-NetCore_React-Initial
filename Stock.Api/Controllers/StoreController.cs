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
using Microsoft.Extensions.Logging;

namespace Stock.Api.Controllers
{
    /// <summary>
    /// Store endpoint.
    /// </summary>
    [Produces("application/json")]
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreService service;
        private readonly ILogger<StoreController> logger;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreController"/> class.
        /// </summary>
        /// <param name="service">Store service.</param>
        /// <param name="mapper">Mapper configurator.</param>
        /// <param name="logger">Logger service.</param>
        public StoreController(StoreService service, IMapper mapper, ILogger<StoreController> logger)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        /// <summary>
        /// Adds a store.
        /// </summary>
        /// <param name="value">Store info.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] StoreDTO value)
        {
            TryValidateModel(value);

            try
            {
                var store = mapper.Map<Store>(value);
                service.Create(store);
                value.Id = store.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch (Exception ex)
            {   
                logger.LogCritical(ex.StackTrace);
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        /// <summary>
        /// Gets all stores.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<StoreDTO>> Get()
        {
            try
            {
                var result = service.GetAll();
                return mapper.Map<IEnumerable<StoreDTO>>(result).ToList();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets a store by id.
        /// </summary>
        /// <param name="id">Store id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<StoreDTO> Get(string id)
        {
            try
            {
                var result = service.Get(id);
                return mapper.Map<StoreDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates a store.
        /// </summary>
        /// <param name="id">Store id.</param>
        /// <param name="value">Store information.</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] StoreDTO value)
        {
            var store = service.Get(id);
            TryValidateModel(value);
            mapper.Map<StoreDTO, Store>(value, store);
            service.Update(store);
        }

        /// <summary>
        /// Deletes a store.
        /// </summary>
        /// <param name="id">Store id to delete</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var store = service.Get(id);
            if (store is null)
                return NotFound();
            
            service.Delete(store);
            return Ok(new { Success = true, Message = "", data = id });
        }

        /// <summary>
        /// Search some stores.
        /// </summary>
        /// <param name="model">Store filters.</param>
        /// <returns></returns>
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

            var stores = service.Search(filter);
            return Ok(stores);
        }
    }
}