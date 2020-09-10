using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService service;
        private readonly IMapper mapper;
        
        public ProductController(ProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>Permite recuperar todas las instancias del Producto</summary>
        /// <returns>Devuelve una colección de instancias de tipo Producto</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return this.mapper.Map<IEnumerable<ProductDTO>>(result).ToList();
            }
            catch (Exception) { return StatusCode(500); }
        }
		
        /// <summary>Permite recuperar una instancia de Producto utilizando un identificador</summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Devuelve una instancia de Producto acorde a su identificador</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                return this.mapper.Map<ProductDTO>(result);
            }
            catch (Exception) { return NoContent(); }
        }
		
        /// <summary>Permite crear una nueva instancia de tipo Producto</summary>
        /// <param name="value">Una instancia de tipo Producto</param>
        /// <returns>Devuelve un mensaje indicando que la instancia de tipo Producto fue exitosamente creada</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO value)
        {
            TryValidateModel(value);

            try
            {
                var product = this.mapper.Map<Product>(value);
                this.service.Create(product);
                value.Id = product.Id;
                return Ok(new { Success = true, Message = "Product succesfully created", data = value });
            }
            catch (System.Exception err) { return Ok(new { Success = false, Message = err.Message.ToString() }); }
        }
		
        /// <summary>Permite editar una instancia de tipo Producto utilizando un identificador</summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProductDTO value)
        {
            try
            {
                var product = this.service.Get(id);
                if (product == null) { throw new System.Exception("Product not found"); }
                TryValidateModel(value);
                this.mapper.Map<ProductDTO, Product>(value, product);
                product.Id = id;
                this.service.Update(product);
                return Ok(new { Success = true, Message = "Product succesfully updated", data = value });
            }

            catch (System.Exception err) { return NotFound(new { Success = false, Message = err.Message.ToString()}); }
        }
		
        /// <summary>Permite borrar una instancia de Producto utilizando un identificador</summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
			try
			{
				var vendor = this.service.Get(id);
				this.service.Delete(vendor);
				return Ok(new { Success = true, Message = "Product succesfully deleted", data = id });
			}
            catch { return NotFound(new { Success = false, Message = "Product was unable to be deleted" }); }
        }
	}
}