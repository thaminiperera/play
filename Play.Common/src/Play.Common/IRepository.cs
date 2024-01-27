//For interacting with a MongoDB database to perform CRUD operations on items in the catalog

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;



namespace Play.Common
{
    
    
    public interface IRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task RemoveAsync(Guid id);
    }
    


}