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
        /// Permite recuperar todas las instancias
        /// </summary>
        /// <returns>Una colección de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeDTO>> Get()
        {
            return Ok(mapper.Map<IEnumerable<ProductTypeDTO>>(service.GetAll()).ToList());
        }

        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductTypeDTO> Get(string id)
        {
            return Ok(mapper.Map<ProductTypeDTO>(service.Get(id)));
        }

        /// <summary>
        /// Permite crear una nueva instancia
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public ProductType Post([FromBody] ProductTypeDTO value)
        {
            TryValidateModel(value);
            var productType = service.Create(mapper.Map<ProductType>(value));
            return mapper.Map<ProductType>(productType);
        }

        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductTypeDTO value)
        {
            var productType = service.Get(id);
            TryValidateModel(value);
            mapper.Map<ProductTypeDTO, ProductType>(value, productType);
            service.Update(productType);
        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
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
