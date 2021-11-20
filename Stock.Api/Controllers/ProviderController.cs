using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stock.Api.Controllers
{
    /// <summary>
    /// Provider endpoint.
    /// </summary>
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService service;
        private readonly IMapper mapper;
        private readonly ILogger<ProviderController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderController"/> class.
        /// </summary>
        /// <param name="service">Provider service.</param>
        /// <param name="mapper">Mapper configurator.</param>
        /// <param name="logger">Logger service.</param>
        public ProviderController(ProviderService service, IMapper mapper, ILogger<ProviderController> logger)
        {
            this.service = service ?? throw new ArgumentException(null, nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(null, nameof(mapper));
            this.logger = logger ?? throw new ArgumentException(null, nameof(logger));
        }

        /// <summary>
        /// Adds a provider.
        /// </summary>
        /// <param name="dto">Provider information.</param>
        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO dto)
        {
            TryValidateModel(dto);

            try
            {
                var providerValues = mapper.Map<Provider>(dto);
                var newProvider = service.Create(providerValues);

                dto.Id = newProvider.Id;

                return Ok(new { Success = true, Message = "", Data = dto });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.StackTrace);
                return Ok(new { Success = false, ex.Message });
            }
        }

        /// <summary>
        /// Get all providers.
        /// </summary>
        /// <returns>A collection of <see cref="ProviderDTO"/>.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            var providers = service.GetAll();
            var mappedProviders = mapper.Map<IEnumerable<ProviderDTO>>(providers);

            return Ok(mappedProviders);
        }

        /// <summary>
        /// Gets a provider by id.
        /// </summary>
        /// <param name="id">Provider id to return.</param>
        /// <returns>A <see cref="ProviderDTO"/>.</returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            var provider = service.Get(id);
            var mappedProvider = mapper.Map<ProviderDTO>(provider);

            return Ok(mappedProvider);
        }

        /// <summary>
        /// Updates a provider.
        /// </summary>
        /// <param name="id">Provider id to edit.</param>
        /// <param name="dto">Provider information.</param>
        [HttpPut("id")]
        public ActionResult Put(string id, [FromBody] ProviderDTO dto)
        {
            TryValidateModel(dto);

            var provider = service.Get(id);

            if (provider is null)
                return NotFound();

            mapper.Map(dto, provider);
            service.Update(provider);

            return NoContent();
        }

        /// <summary>
        /// Deletes a provider.
        /// </summary>
        /// <param name="id">Provider id to delete.</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = service.Get(id);

            if (provider is null)
                return NotFound();

            service.Delete(provider);

            return Ok(new { Success = true, Message = "", Data = id });
        }

        /// <summary>
        /// Search providers by filters.
        /// </summary>
        /// <param name="model">Provider filters.</param>
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

            var providers = service.Search(filter);

            return Ok(providers);
        }
    }
}
