using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;

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
        public void Put(string id, [FromBody] ProviderDTO dto)
        {
            TryValidateModel(dto);

            var provider = service.Get(id);

            mapper.Map(dto, provider);
            service.Update(provider);
        }
    }
}
