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

        public ProviderController(ProviderService service, ILogger<ProviderController> logger, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.logger = logger ?? throw new ArgumentException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var result = service.Get(id);
                if (result == null)
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
                return mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
            }
            catch (Exception ex)
            {
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
            catch(Exception ex)
            {
                logger.LogCritical(ex.StackTrace);
                return Ok(new { Success = false, Message = "The id is aleready in use" });
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

        
        ///name
        ///email
        ///phone
        ///action
        
        [HttpPost("search")]
        public ActionResult Search([FromBody] ProviderSearchDTO model)
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Action.Equals(ActionDto.AND));
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                filter = filter.AndOrCustom(
                    x => x.Email.ToUpper().Contains(model.Email.ToUpper()),
                    model.Action.Equals(ActionDto.AND));
            }

            if (!string.IsNullOrWhiteSpace(model.Phone))
            {
                filter = filter.AndOrCustom(
                    x => x.Phone.ToUpper().Contains(model.Phone.ToUpper()),
                    model.Action.Equals(ActionDto.AND));
            }


            var providers = service.Search(filter);
            var providersDTO = mapper.Map<List<ProviderDTO>>(providers);
            return Ok(providersDTO);
        }
        


    }
}
