using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderController"/> class.
        /// </summary>
        /// <param name="service">Provider service.</param>
        /// <param name="mapper">Mapper configurator.</param>
        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentException(null, nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(null, nameof(mapper));
        }

        /// <summary>
        /// Add a provider.
        /// </summary>
        /// <param name="dto">Provider information.</param>
        /// <returns>A <see cref="Provider"/>.</returns>
        [HttpPost]
        public Provider Post([FromBody] ProviderDTO dto)
        {
            TryValidateModel(dto);

            var providerValues = mapper.Map<Provider>(dto);
            var provider = service.Create(providerValues);

            return provider;
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

            return Ok();
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

            if (!string.IsNullOrWhiteSpace(model.Phone))
            {
                filter = filter.AndOrCustom(
                    x => x.Phone.ToUpper().Contains(model.Phone.ToUpper()),
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
