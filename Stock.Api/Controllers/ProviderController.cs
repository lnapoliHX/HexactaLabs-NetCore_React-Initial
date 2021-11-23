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
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService service;
        private readonly ILogger<ProviderController> logger;
        private readonly IMapper mapper;

        public ProviderController(ProviderService service, IMapper mapper, ILogger<ProviderController> logger)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        [HttpPost]
        public ActionResult CreateProvider([FromBody] ProviderDTO providerDTO)
        {
            TryValidateModel(providerDTO);

            try
            {
                var provider = mapper.Map<Provider>(providerDTO);
                service.Create(provider);
                providerDTO.Id = provider.Id;
                return Ok(new { Success = true, Message = "", data = providerDTO });
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.StackTrace);
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> GetAll()
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

        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> GetById(string id)
        {
            try
            {
                var result = service.Get(id);
                return mapper.Map<ProviderDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public void UpdateProvider(string id, [FromBody] ProviderDTO value)
        {
            var provider = service.Get(id);
            TryValidateModel(provider);
            mapper.Map<ProviderDTO, Provider>(value, provider);
            service.Update(provider);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = service.Get(id);
            if(provider == null)
            {
                return NotFound();
            }
            service.Delete(provider);
            return Ok(new { Success = true, Message = "", data = id });
        }

        [HttpPost("searchProvider")]
        public ActionResult SearchProvider([FromBody] ProviderSearchDTO model)
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                        x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                        model.Condition.Equals(ActionDto.AND));
            }
            var provider = service.Search(filter);
            return Ok(provider);
        }


    }
}
