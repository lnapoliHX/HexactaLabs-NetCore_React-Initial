using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Stock.Api.Domain.Exceptions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System.Net;

namespace Stock.AppService.Services
{
    public class ProviderService : BaseService<Provider>, IProviderService
    {
        private readonly ILogger<ProviderService> _logger;

        public ProviderService(IRepository<Provider> repository, ILogger<ProviderService> logger)
            : base(repository)
        {
            _logger = logger;
        }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Provider CreateEntity(Provider entity)
        {
            if (entity == null)
            {
                throw new BusinessException(
                    $"The given entity is invalid. Please retry",
                    HttpStatusCode.BadRequest);
            }

            if (!IsUniqueName(entity.Name))
            {
                throw new BusinessException(
                    $"The name {entity.Name} is already in use. Please use a different name and try again.",
                    HttpStatusCode.Conflict);
            }

            _logger.LogDebug(
                $"Adding a new Entity with Id: {entity.Id}, Name: {entity.Name}, email: {entity.Email}, Phone: {entity.Phone}");

            return base.Create(entity);
        }

        /// <summary>
        /// Updates a DTO
        /// </summary>
        /// <param name="idToChange"></param>
        /// <param name="newDto"></param>
        public Provider UpdateEntity(string idToChange, Provider newDto)
        {
            var provider = this.Get(idToChange);

            // If provider null, or not unique name we throw
            if (provider == null)
            {
                throw new BusinessException("The Provider you're trying to change doesn't exist.",
                    HttpStatusCode.NotFound);
            }

            if (!IsUniqueName(newDto.Name))
            {
                throw new BusinessException(
                    $"The name {newDto.Name} is already in use. Please use a different name and try again.",
                    HttpStatusCode.Conflict);
            }

            // Build the new provider
            provider = new Provider
            {
                Email = newDto.Email,
                Id = provider.Id,
                Name = newDto.Name,
                Phone = newDto.Phone
            };

            _logger.LogDebug(
                $"Updating an Entity with Id: {provider.Id}, Name: {provider.Name}, email: {provider.Email}, Phone: {provider.Phone}");

            return this.Update(provider);
        }

        /// <summary>
        /// Deletes a value in the DB
        /// </summary>
        /// <param name="idToDelete"></param>
        public void DeleteEntity(string idToDelete)
        {
            var provider = this.Get(idToDelete);

            if (provider == null)
            {
                throw new BusinessException("The Provider you're trying to delete doesn't exist.",
                    HttpStatusCode.NotFound);
            }

            _logger.LogDebug(
                $"Deleting an Entity with Id: {provider.Id}, Name: {provider.Name}, email: {provider.Email}, Phone: {provider.Phone}");

            this.Delete(provider);
        }

        /// <summary>
        /// Search in the repository following a specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter)
        {
            return this.Repository.List(filter);
        }

        /// <summary>
        /// Checks for the Name to be unique in the current DB
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True if the given name is unique, false if empty or already there.</returns>
        private bool IsUniqueName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }
    }
}