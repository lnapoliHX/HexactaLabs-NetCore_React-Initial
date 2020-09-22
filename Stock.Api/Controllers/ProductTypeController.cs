﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stock.Api.DTOs;
using Stock.AppService.Services;
using Stock.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/producttype")]
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

        ///<summary>
        /// Permite recuperar todas las instancias
        ///</summary>
        /// <returns>Una colección de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeDTO>> Get()
        {
            //Agrego manejo de excepciones y devolver en Ok() 
            try{
                var resul = this.service.GetAll();
                return Ok(this.mapper.Map<IEnumerable<ProductTypeDTO>>(this.service.GetAll()).ToList());
                //return this.mapper.Map<IEnumerable<ProductTypeDTO>>(this.service.GetAll()).ToList();
            }
            catch(Exception){
                //return StatusCode(500);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductTypeDTO> Get(string id)
        {
            //Agrego el manejo de excepciones y retornar en OK();
            try{
                var result = this.service.Get(id);
                return Ok(this.mapper.Map<ProductTypeDTO>(this.service.Get(id)));
            }
            catch(Exception){
                //return StatusCode(500);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            //return this.mapper.Map<ProductTypeDTO>(this.service.Get(id));
            //return Ok(this.mapper.Map<ProductTypeDTO>(this.service.Get(id)));
        }

        /// <summary>
        /// Permite crear una nueva instancia
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public ProductType Post([FromBody] ProductTypeDTO value)
        {
            TryValidateModel(value);
            var productType = this.service.Create(this.mapper.Map<ProductType>(value));
            
            return this.mapper.Map<ProductType>(productType);
        }

        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ProductTypeDTO value)
        {
            var productType = this.service.Get(id);
            TryValidateModel(value);
            this.mapper.Map<ProductTypeDTO, ProductType>(value, productType);
            this.service.Update(productType);
        }


        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var productType = this.service.Get(id);

             Expression<Func<Product, bool>> filter = x => x.ProductType.Id.Equals(id);
            
            this.service.Delete(productType);
            return Ok();
        }
    }
}
