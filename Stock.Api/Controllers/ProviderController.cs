using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private ProviderService service;
        private readonly IMapper mapper;
        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        /// <summary>
        /// Permite Recuperar Todas las Instacias
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Permite Crear un Provider
        /// </summary>
        /// <param name ="value">Una Instancia</param> 
        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value){
            TryValidateModel(value);
            try{
                var provider = this.mapper.Map<Provider>(value);
                this.service.Create(provider);
                value.Id = provider.Id;
                return Ok(new {Success = true, Message = "", data = value});
            }
            catch{
                return Ok(new {Success = false, Message = "The name is already in used"});
            }
        }
    }
}