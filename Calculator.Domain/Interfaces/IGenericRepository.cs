using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calculator.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Guid id);
        Task<T> Update(Guid id, T entity);
    }
}
