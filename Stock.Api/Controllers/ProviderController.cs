using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;

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
        private readonly ProviderService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<ProviderController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeController"/> class.
        /// </summary>
        /// <param name="service">Product type service.</param>
        /// <param name="mapper">Mapper configurator.</param>
        public ProviderController(ProviderService service, IMapper mapper, ILogger<ProviderController> logger)
        {
            _service = service ?? throw new ArgumentException(nameof(service));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        /// <summary>
        /// Gets a provider by id.
        /// </summary>
        /// <param name="id">Product id to return.</param>
        /// <returns>A <see cref="ProviderDTO"/></returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                return Ok(_mapper.Map<ProviderDTO>(_service.Get(id)));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
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
                return Ok(_mapper.Map<IEnumerable<ProviderDTO>>(_service.GetAll()).ToList());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Add a provider.
        /// </summary>
        /// <param name="value">Provider information.</param>
        [HttpPost]
        public Provider Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);
            var provider = _service.Create(_mapper.Map<Provider>(value));
            return _mapper.Map<Provider>(provider);
        }

        /// <summary>
        /// Updates a provider.
        /// </summary>
        /// <param name="id">Provider id to edit.</param>
        /// <param name="value">Provider information.</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProviderDTO value)
        {
            var provider = _service.Get(id);
            TryValidateModel(value);
            _mapper.Map<ProviderDTO, Provider>(value, provider);
            _service.Update(provider);
        }

        /// <summary>
        /// Deletes a provider.
        /// </summary>
        /// <param name="id">Provider id to delete.</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = _service.Get(id);

            if (provider is null)
                return NotFound();

            _service.Delete(provider);
            return Ok(new { Success = true, Message = "", data = id });
        }

        /// <summary>
        /// Search some providers.
        /// </summary>
        /// <param name="model">Provider filters.</param>
        /// <returns></returns>
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

            var providers = _service.Search(filter);
            return Ok(providers);
        }
    }
}
