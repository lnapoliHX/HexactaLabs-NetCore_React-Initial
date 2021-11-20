using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.Api.Extensions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using Microsoft.Extensions.Logging;

namespace Stock.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService service;
        private readonly ILogger<ProductController> logger;
        private readonly IMapper mapper;

        public ProductController(ProductService service, IMapper mapper, ILogger<ProductController> logger)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        // POST
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
            TryValidateModel(value);

            try
            {
                var product = mapper.Map<Product>(value);
                service.Create(product);
                value.Id = product.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch (Exception ex)
            {   
                logger.LogCritical(ex.StackTrace);
                return Ok(new { Success = false, Message = "The name is already in use" });
            }
        }

        // GET all
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            try
            {
                var result = service.GetAll();
                return mapper.Map<IEnumerable<ProductDTO>>(result).ToList();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // GET for id
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            try
            {
                var result = service.Get(id);
                return mapper.Map<ProductDTO>(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // UPDATE for id
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductDTO value)
        {
            var product = service.Get(id);
            TryValidateModel(value);
            mapper.Map<ProductDTO, Product>(value, product);
            service.Update(product);
        }

        // DELETE for id
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var product = service.Get(id);
            if (product is null)
                return NotFound();
            
            service.Delete(product);
            return Ok(new { Success = true, Message = "", data = id });
        }

    }
}