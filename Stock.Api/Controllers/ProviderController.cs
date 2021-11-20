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
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService service;
        private readonly IMapper mapper;

        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentException(null, nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(null, nameof(mapper));
        }

        [HttpPost]
        public Provider Post([FromBody] ProviderDTO dto)
        {
            TryValidateModel(dto);

            var providerValues = mapper.Map<Provider>(dto);
            var provider = service.Create(providerValues);

            return provider;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            var providers = service.GetAll();
            var mappedProviders = mapper.Map<ProviderDTO>(providers);

            return Ok(mappedProviders);
        }

        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            var provider = service.Get(id);
            var mappedProvider = mapper.Map<ProviderDTO>(provider);

            return Ok(mappedProvider);
        }

        [HttpPut("id")]
        public ActionResult Put(string id, [FromBody] ProviderDTO dto)
        {
            TryValidateModel(dto);

            var provider = service.Get(id);

            mapper.Map(dto, provider);
            service.Update(provider);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = service.Get(id);

            if (provider is null)
                return NotFound();

            service.Delete(provider);

            return Ok();
        }

        [HttpPost("search")]
        public ActionResult Search([FromBody] ProviderSearchDTO model)
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(x => x.Name.ToUpper().Contains(model.Name.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(model.Phone))
            {
                filter = filter.AndOrCustom(x => x.Phone.ToUpper().Contains(model.Phone.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                filter = filter.AndOrCustom(x => x.Email.ToUpper().Contains(model.Email.ToUpper()));
            }

            var providers = service.Search(filter);

            return Ok(providers);
        }
    }
}
