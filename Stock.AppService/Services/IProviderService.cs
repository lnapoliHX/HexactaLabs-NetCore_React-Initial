using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.Model.Entities;

namespace Stock.AppService.Services
{
    public interface IProviderService
    {
        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Provider CreateEntity(Provider entity);

        /// <summary>
        /// Updates a DTO
        /// </summary>
        /// <param name="idToChange"></param>
        /// <param name="newDto"></param>
        Provider UpdateEntity(string idToChange, Provider newDto);

        /// <summary>
        /// Deletes a value in the DB
        /// </summary>
        /// <param name="idToDelete"></param>
        void DeleteEntity(string idToDelete);

        /// <summary>
        /// Search in the repository following a specified filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<Provider> Search(Expression<Func<Provider, bool>> filter);

        Provider Create(Provider entity);

        IEnumerable<Provider> GetAll();

        Provider Get(string id);

        void Delete(Provider entity);

        Provider Update(Provider entity);
    }
}