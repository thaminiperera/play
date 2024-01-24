//For interacting with a MongoDB database to perform CRUD operations on items in the catalog

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Play.Catalog.Service.Entities;


namespace Play.Catalog.Service.Repositories
{
    
    
    public interface IRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task RemoveAsync(Guid id);
    }
    


}