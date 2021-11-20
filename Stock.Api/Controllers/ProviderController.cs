using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;

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
    }
}
