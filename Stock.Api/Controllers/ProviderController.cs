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
using System.Data;




namespace Stock.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ProviderService service;
        private readonly IMapper mapper;

        public ProviderController(ProviderService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>
        /// Permite recuperar todas las instancias
        /// </summary>
        /// <returns>Una colección de instancias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProviderDTO>> Get()
        {
            try
            {
                var result = this.service.GetAll();
                var lista = this.mapper.Map<IEnumerable<ProviderDTO>>(result).ToList();
                return lista;
            }
            catch (DuplicateNameException)
            {
                return StatusCode(500);
            };

        }

        /// <summary>
        /// Permite recuperar una instancia mediante un identificador
        /// </summary>
        /// <param name="id">Identificador de la instancia a recuperar</param>
        /// <returns>Una instancia</returns>
        [HttpGet("{id}")]
        public ActionResult<ProviderDTO> Get(string id)
        {
            try
            {
                var result = this.service.Get(id);
                var datos = this.mapper.Map<ProviderDTO>(result);
                if (datos != null)
                {
                    return Ok(new { Succes = true, data = datos });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = " No Encontrado" });
                }
            }
            catch (AutoMapperMappingException)
            {
                return BadRequest(new { Success = false, Message = "No se encontro el proveedor" });
            }
        }

        /// <summary>
        /// Permite crear una nueva instancia
        /// </summary>
        /// <param name="value">Una instancia</param>
        [HttpPost]
        public ActionResult Post([FromBody] ProviderDTO value)
        {
            TryValidateModel(value);

            try
            {
                var provider = this.mapper.Map<Provider>(value);
                this.service.Create(provider);
                value.Id = provider.Id;
                return Ok(new { Success = true, Message = "", data = value });
            }
            catch (DuplicateNameException)
            {
                return BadRequest(new { Success = false, Message = "Este Proveedor ya fue creado" });
            }
        }

        /// <summary>
        /// Permite editar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a editar</param>
        /// <param name="value">Una instancia con los nuevos datos</param>

        [HttpPut("{id}")]
        [ResponseCache]
        public ActionResult Put(string id, [FromBody] ProviderDTO value)
        {

            var provider = this.service.Get(id);
            TryValidateModel(value);
            try
            {
                this.mapper.Map<ProviderDTO, Provider>(value, provider);
                if (provider == null)
                {
                    return StatusCode(404);
                }
                else
                {
                    this.service.Update(provider);

                    return Ok(new { Success = true, Message = "Proveedor Actualizado con Exito" });
                }
            }
            catch (ArgumentException)
            {
                return BadRequest(new { Success = false, Message = "No se Pudo actualizar", data = "" });

            };

        }

        /// <summary>
        /// Permite borrar una instancia
        /// </summary>
        /// <param name="id">Identificador de la instancia a borrar</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var provider = this.service.Get(id);
            try
            {
                this.service.Delete(provider);
                return Ok(new { Success = true, Message = "Eliminado con Exito", data = id });
            }
            catch (DataException)
            {
                return BadRequest(new { Success = false, Message = "No se pudo eliminar el provedor", data = "" });
            }

        }



        /// <summary>
        /// Permite Buscar una instancia entre las creadas
        /// </summary>
        /// <param name="model">Se extrae del modelo el nombre y el Email</param>


        [HttpPost("search")]
        public ActionResult Search([FromBody] ProviderSearchDTO model)
        {
            Expression<Func<Provider, bool>> filter = x => !string.IsNullOrWhiteSpace(x.Id);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                filter = filter.AndOrCustom(
                    x => x.Name.ToUpper().Contains(model.Name.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                filter = filter.AndOrCustom(
                    x => x.Email.ToUpper().Contains(model.Email.ToUpper()),
                    model.Condition.Equals(ActionDto.AND));
            }
            try
            {
                var providers = this.service.Search(filter);
                return Ok(providers);
            }
            catch (DataException)
            {
                return BadRequest(new { Success = true, Message = "No se Encontro el proveedor" }); ;
            }


        }
    }
}
