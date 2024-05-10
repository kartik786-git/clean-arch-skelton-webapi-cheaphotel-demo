using Application.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal readonly HotelDbContext _hotelDbContext;

        public Repository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
          return  await _hotelDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T? result = await _hotelDbContext.Set<T>().FindAsync(id);
            return result;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _hotelDbContext.Set<T>().AddAsync(entity);
            await _hotelDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _hotelDbContext.Set<T>().Remove(entity);
            await _hotelDbContext.SaveChangesAsync();
        }

       

        public async Task UpdateAsync(T entity)
        {
            _hotelDbContext.Entry(entity).State = EntityState.Modified;
            await _hotelDbContext.SaveChangesAsync();
        }
    }
}
