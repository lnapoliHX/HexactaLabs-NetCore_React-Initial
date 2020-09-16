using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        /// Permite recuperar todas las instancias
        /// </summary>
        /// <returns>Una colección de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductTypeDTO>> Get()
        {
            return this.mapper.Map<IEnumerable<ProductTypeDTO>>(this.service.GetAll()).ToList();
        }

        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductTypeDTO> Get(string id)
        {
            return this.mapper.Map<ProductTypeDTO>(this.service.Get(id));
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

        /// <summary>Permite efectuar una busqueda personalizada de Categorias de productos</summary>
        /// <param name="model">Atributo de tipo de producto sobre el cual efectuar la busqueda</param>
        /// <returns>Devuelve la instancia encontrada</returns>
		/// <exception cref="System.Exception">Arroja error cuando los criterios provistos no son suficientes para encontrar el tipo de producto</exception>
        [HttpPost("search")]
        public ActionResult Search([FromBody] ProductTypeSearchDTO model)
        {
			try
			{
				Expression<Func<ProductType, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);
				
				//Busqueda por iniciales
				if (!string.IsNullOrWhiteSpace(model.Initials))
				{ filter = filter.AndOrCustom(x => x.Initials.ToUpper().Contains(model.Initials.ToUpper()), model.Condition.Equals(ActionDto.AND)); }
				
				//Busqueda por descripcion
				if (!string.IsNullOrWhiteSpace(model.Description))
				{ filter = filter.AndOrCustom(p => p.Description.Contains(model.Description), model.Condition.Equals(ActionDto.AND)); }
				
				var category = this.service.Search(filter);
				if (category == null) { throw new System.Exception("There is no product type found by given criteria"); }
				return Ok(category);
			}
			catch (System.Exception err) { return NotFound(new { Success = false, Message = err.Message.ToString() }); }
        }
    }
}
