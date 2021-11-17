using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {

        private readonly ProviderService service;
        private readonly IMapper mapper;

        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }


        /// <summary>
        /// Get all providers.
        /// </summary>
        /// <returns>List of <see cref="ProviderDTO"/></returns>
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
        /// Gets a provider by id.
        /// </summary>
        /// <param name="id">Provider id to return.</param>
        /// <returns>A <see cref="ProviderDTO"/></returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            return Ok(mapper.Map<ProviderDTO>(service.Get(id)));
        }

        /// <summary>
        /// Add a provider
        /// </summary>
        /// <param name="value">Provider information.</param>
        [HttpPost]
        public Provider Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);
            var provider = service.Create(mapper.Map<Provider>(value));
            return mapper.Map<Provider>(provider);
        }

        /// <summary>
        /// Updates a provider.
        /// </summary>
        /// <param name="id">Provider id to edit.</param>
        /// <param name="value">Prodvider information.</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProviderDTO value)
        {
            var provider = service.Get(id);
            TryValidateModel(value);
            try
            {
                mapper.Map<ProviderDTO, Provider>(value, provider);
                service.Update(provider);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;             
            }
        }

        /// <summary>
        /// Deletes a provider
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
        /// Search by Id, Name, Phone, Email
        /// </summary>
        /// <param name="model">Provider information.</param>        
        /// <returns>A <see cref="ProviderSearchDTO"/></returns>
        [HttpGet]
        [Route("search")]
        public ActionResult Search([FromQuery] ProviderSearchDTO model) 
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            // Search by Id
            if (!string.IsNullOrWhiteSpace(model.Id))
            {
                filter = filter.AndOrCustom(
                    x => x.Id.ToUpper().Contains(model.Id.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            // Search by Name
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            // Search by Phone
            if (!string.IsNullOrWhiteSpace(model.Phone))
            {
                filter = filter.AndOrCustom(
                    x => x.Phone.ToUpper().Contains(model.Phone.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            // Search by Email
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                filter = filter.AndOrCustom(
                    x => x.Email.ToUpper().Contains(model.Email.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            var provider = service.Search(filter);
            
            return Ok(provider);
        }
    }
}
