using Management.Application.Contracts;
using Management.Application.Models;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Management.Infrastructure.Repositories
{
    
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DatabaseContext _context;

        public BaseRepository(DatabaseContext context)
        {
            _context= context;
        }
        public async Task<T> GetAsync(object obj)
        {
            // var search = await _context.FindAsync<T>(email).ConfigureAwait(false);
            return await _context.Set<T>().FindAsync(obj);
  
        }

        public async Task<T> GetAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);

        }

        public async Task<IEnumerable<T>> GetAllPagedAsync(RequestParameters parameter)
        {
            return await _context.Set<T>().ToPagedListAsync(parameter._pageIndex, parameter.PageSize);

        }
        public virtual async Task CreateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            var result = _context.Set<T>().Remove(entity);
            if(result.State == EntityState.Deleted)
            {
                await _context.SaveChangesAsync();
                return true;
            }              
                return false;

        }

        public async Task<T> UpdateAsync(T entity, int Id)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
           await _context.SaveChangesAsync();
            return await Task.FromResult(entity);
            //return await Task.CompletedTask();

        }
    }
}
