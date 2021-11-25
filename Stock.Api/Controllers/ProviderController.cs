using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    /// <summary>
    /// Provider endpoint.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService service;
        private readonly ILogger<ProviderController> logger;
        private readonly IMapper mapper;

        public ProviderController(ProviderService service, IMapper mapper, ILogger<ProviderController> logger)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        /// <summary>
        /// Adds a provider.
        /// </summary>
        /// <param name="value">Provider info.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);

            try
            {
                var store = mapper.Map<Provider>(value);
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
        /// Gets all Providers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = service.GetAll();
                return mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Gets a Provider by id.
        /// </summary>
        /// <param name="id">Provider id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var result = service.Get(id);
                return mapper.Map<ProviderDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates a Provider.
        /// </summary>
        /// <param name="id">Provider id.</param>
        /// <param name="value">Provider information.</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProviderDTO value)
        {
            var store = service.Get(id);
            TryValidateModel(value);
            mapper.Map<ProviderDTO, Provider>(value, store);
            service.Update(store);
        }

        /// <summary>
        /// Deletes a Provider.
        /// </summary>
        /// <param name="id">Provider id to delete</param>
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
        /// Search some Providers.
        /// </summary>
        /// <param name="model">Store filters.</param>
        /// <returns></returns>
        [HttpPost("search")]
        public ActionResult Search([FromBody] ProviderSearchDTO model)
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrWhiteSpace(model.Phone))
            {
                filter = filter.AndOrCustom(
                    x => x.Phone.ToUpper().Contains(model.Phone.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

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

            var providers = service.Search(filter);
            return Ok(providers);
        }
    }
}
