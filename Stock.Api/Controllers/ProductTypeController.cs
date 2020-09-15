using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
    [Route("api/productType")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly ProductTypeService service;
        private readonly IMapper mapper;
        
        public ProductTypeController(ProductTypeService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>
        /// Permite recuperar todos los Tipos de Productos
        /// </summary>
        /// <returns>Una colección de Tipos de Productos o un código en caso de error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                return Ok(new {Success = true, Message = "List of all Product Types", 
                                ProductTypes = this.mapper.
                                               Map<IEnumerable<ProductTypeDTO>>(result).ToList()} );
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        /*
        {
            return this.mapper.Map<IEnumerable<ProductTypeDTO>>(this.service.GetAll()).ToList();
        } */

        /// <summary>
        /// Permite recuperar las propiedades de un Tipo de Producto mediante su Id
        /// </summary>
        /// <param name="id">Identificador del Tipo de Producto a recuperar</param>
        /// <returns>Una instancia de Tipo de Producto o un código en caso de error</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductTypeDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                return Ok(new { Success = true, 
                                Message = "Product Type Obtained!", 
                                ProductType = this.mapper.Map<ProductTypeDTO>(result) });

            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        // {
        //     return this.mapper.Map<ProductTypeDTO>(this.service.Get(id));
        // }

        /// <summary>
        /// Permite agregar un nuevo Tipo de Producto al repositorio
        /// </summary>
        /// <param name="value">Propiedades del Tipo de Producto</param>
        /// <returns>Exito o Error y el Tipo de Producto que se intentó crear</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ProductTypeDTO value)
        {
            TryValidateModel(value);

            try
            {
                var productType = this.mapper.Map<ProductType>(value);
                this.service.Create(productType);
                value.Id = productType.Id;
                return Ok(new { Success = true, Message = "Product Type Created!", 
                                ProductType = value });
            }
            catch
            {
                return Ok(new { Success = false, 
                                Message = "The initials or description are already in use!!!", 
                                ProductType = value });
            }
        }
        // {
        //     TryValidateModel(value);
        //     var productType = this.service.Create(this.mapper.Map<ProductType>(value));
        //     return this.mapper.Map<ProductType>(productType);
        // }

        /// <summary>
        /// Permite actualizar las propiedades de un Tipo de Producto
        /// </summary>
        /// <param name="id">Identificador del Tipo de Producto a actualizar</param>
        /// <param name="value">Propiedades del Tipo de Producto</param>
        /// <returns>Exito o Error y el Tipo de Producto que se intentó actualizar</returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ProductTypeDTO value)
        {
            TryValidateModel(value);

            var productType = this.service.Get(id);
            try
            {
                this.mapper.Map<ProductTypeDTO, ProductType>(value, productType);
                this.service.Update(productType);
                return Ok(new { Success = true, Message = "Product Type Updated!", 
                                ProductType = productType });
            }
            catch
            {
                return Ok(new { Success = false, 
                                Message = "The initials or description are already in use!!!", 
                                ProductType = productType });
            }
            
        }
        // {
        //     var productType = this.service.Get(id);
        //     TryValidateModel(value);
        //     this.mapper.Map<ProductTypeDTO, ProductType>(value, productType);
        //     this.service.Update(productType);
        // }

        /// <summary>
        /// Permite borrar un Tipo de Producto
        /// </summary>
        /// <param name="id">Identificador del Tipo de Producto a borrar</param>
        /// <returns>Exito y el Id del Tipo de Producto eliminado o un código en caso de error</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var productType = this.service.Get(id);

            //Expression<Func<Product, bool>> filter = x => x.ProductType.Id.Equals(id);
            
            try
            {          
                this.service.Delete(productType);
                return Ok(new { Success = true, Message = "Product Type Deleted!", data = id });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            //return Ok();
        }
    }
}
