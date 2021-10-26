using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Api.Controllers
{
    /// <summary>
    /// Product type endpoint.
    /// </summary>
    [Produces("application/json")]
    [Route("api/producttype")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly ProductTypeService service;
        private readonly IMapper mapper;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeController"/> class.
        /// </summary>
        /// <param name="service">Product type service.</param>
        /// <param name="mapper">Mapper configurator.</param>
        public ProductTypeController(ProductTypeService service, IMapper mapper)
        {
            this.service = service ?? throw new ArgumentException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>List of <see cref="ProductTypeDTO"/></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeDTO>> Get()
        {
            return Ok(mapper.Map<IEnumerable<ProductTypeDTO>>(service.GetAll()).ToList());
        }

        /// <summary>
        /// Gets a product by id.
        /// </summary>
        /// <param name="id">Product id to return.</param>
        /// <returns>A <see cref="ProductTypeDTO"/></returns>
        [HttpGet("{id}")]
        public ActionResult<ProductTypeDTO> Get(string id)
        {
            return Ok(mapper.Map<ProductTypeDTO>(service.Get(id)));
        }

        /// <summary>
        /// Add a product.
        /// </summary>
        /// <param name="value">Product information.</param>
        [HttpPost]
        public ProductType Post([FromBody] ProductTypeDTO value)
        {
            TryValidateModel(value);
            var productType = service.Create(mapper.Map<ProductType>(value));
            return mapper.Map<ProductType>(productType);
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="id">Product id to edit.</param>
        /// <param name="value">Product information.</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductTypeDTO value)
        {
            var productType = service.Get(id);
            TryValidateModel(value);
            mapper.Map<ProductTypeDTO, ProductType>(value, productType);
            service.Update(productType);
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">Product id to delete.</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var productType = service.Get(id);
            
            if (productType is null) 
                return NotFound();
            
            service.Delete(productType);
            return Ok();
        }
    }
}
