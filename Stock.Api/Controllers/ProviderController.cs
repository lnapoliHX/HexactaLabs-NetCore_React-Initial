using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Stock.Api.Controllers
{
    /// <summary>
    /// This controller manage all CRUD for a provider
    /// </summary>
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]

    public class ProviderController : ControllerBase
    {
        private readonly ProviderService service;
        private readonly IMapper mapper;
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="providerService">CRUD and search service for a provider</param>
        /// <param name="mapper">Mapper configurator</param>
        public ProviderController(ProviderService providerService, IMapper mapper)
        {
            this.service = providerService ?? throw new ArgumentException(nameof(providerService));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));


        }

        /// <summary>
        /// Get all provider
        /// </summary>
        /// <returns>Return a list of provider</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            return (mapper.Map<IEnumerable<ProviderDTO>>(service.GetAll()).ToList());
        }

        /// <summary>
        /// Get provider by provider id
        /// </summary>
        /// <param name="id">Represent an id of a provider </param>
        /// <returns>Return a provider</returns>
        [HttpGet("{id}")]

        public ActionResult<ProviderDTO> Get(string id)
        {
            return this.mapper.Map<ProviderDTO>(service.Get(id));
        }

        /// <summary>
        /// Create a new provider
        /// </summary>
        /// <param name="value">Represent all information need it for a provider</param>
        /// <returns>Return a provider and its provider id </returns>
        [HttpPost]

        public Provider Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);
            var provider = service.Create(mapper.Map<Provider>(value));
            return mapper.Map<Provider>(provider);

        }

        /// <summary>
        /// Update a provider by provider id
        /// </summary>
        /// <param name="id">Represent an id of a provider</param>
        /// <param name="value">Represent all information need it for a provider</param>
        [HttpPut("{id}")]

        public void Put(string id, [FromBody] ProviderDTO value)
        {
            var provider = service.Get(id);
            TryValidateModel(value);

            mapper.Map<ProviderDTO, Provider>(value, provider);

            service.Update(provider);
        }

        /// <summary>
        /// Delete a provider by provider id
        /// </summary>
        /// <param name="id">Represent an id of a provider</param>
        /// <returns></returns>
        [HttpDelete("{id}")]

        public ActionResult Delete(string id)
        {
            var provider = service.Get(id);
            if (provider == null) { return NotFound(); }

            service.Delete(provider);
            return Ok();
        }

        /// <summary>
        /// Search a provider by criteria
        /// </summary>
        /// <param name="dto">Represent a search of provider by name</param>
        /// <returns>Return a list of provider by criteria</returns>
        [HttpPost("search")]

        public ActionResult<IEnumerable<ProviderDTO>> Search([FromBody] ProviderSearchDTO dto)
        {
            Expression<Func<Provider, bool>> filter = x => dto != null;
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(dto.Name.ToUpper()),
                    dto.Condition.Equals(ActionDto.AND));
            }

            var providers = service.Search(filter);


            return Ok(mapper.Map<IEnumerable<ProviderDTO>>(providers));

        }
    }
}
