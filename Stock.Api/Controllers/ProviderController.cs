using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.Api.DTOs.Provider;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _service;
        private readonly IMapper _mapper;

        public ProviderController(IProviderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAll();
            return Ok(this._mapper.Map<List<ProviderDto>>(result));
        }

        [HttpGet("{id}")]
        public IActionResult SearchById([Required]string id)
        {
            var result = _service.Get(id);
            return Ok(this._mapper.Map<ProviderDto>(result));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProviderDto dto)
        {
            var serviceInput = _mapper.Map<Provider>(dto);

            var result = _service.CreateEntity(serviceInput);

            return CreatedAtAction(nameof(SearchById), new { id = result.Id }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Put([Required]string id, [FromBody] ProviderDto dto)
        {
            // We map the new Provider
            var serviceInput = _mapper.Map<Provider>(dto);

            // We try to Update the given entity
            return Ok(_mapper.Map<ProviderDto>(_service.UpdateEntity(id, serviceInput)));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([Required]string id)
        {
            // We try to delete the given value
            _service.DeleteEntity(id);

            return Ok();
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] ProviderQueryDto model)
        {
            // We build the filter to send towards the Service
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

            var stores = _service.Search(filter);

            // If we find something, we return it as an OK response
            // Otherwise, we return a 204 No Content response
            return stores.Any() ? (IActionResult)Ok(stores) : NoContent();
        }
    }
}