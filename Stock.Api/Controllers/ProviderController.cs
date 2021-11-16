using AutoMapper;
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
    /// Product type endpoint.
    /// </summary>
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService service;
        private readonly ILogger<ProviderController> logger;
        private readonly IMapper mapper;

        public ProviderController(ProviderService service, ILogger<ProviderController> logger, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Gets a provider by id
        /// </summary>
        /// <param name="id">Provider id.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var result = service.Get(id);
                if (result != null)
                    return mapper.Map<ProviderDTO>(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = service.GetAll();
                return mapper.Map<List<ProviderDTO>>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);

            try
            {
                var provider = mapper.Map<Provider>(value);
                service.Create(provider);
                value.Id = provider.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.StackTrace);
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProviderDTO value)
        {
            var provider = service.Get(id);
            TryValidateModel(value);
            mapper.Map<ProviderDTO, Provider>(value, provider);
            service.Update(provider);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = service.Get(id);
            if (provider is null)
                return NotFound();

            service.Delete(provider);
            return Ok(new { Success = true, Message = "", data = id });
        }

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

            if (!string.IsNullOrWhiteSpace(model.Phone))
            {
                filter = filter.AndOrCustom(
                    x => x.Phone.ToUpper().Contains(model.Phone.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            var provider = service.Search(filter);
            return Ok(provider);
        }
    }
}
