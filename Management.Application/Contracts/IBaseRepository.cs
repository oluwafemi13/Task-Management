using Management.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetAsync(object obj);
        Task<T> GetAsync(int Id);
        Task CreateAsync(T entity);
        Task<T> UpdateAsync(T entity, int Id);
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllPagedAsync(RequestParameters parameter);
    }
}
