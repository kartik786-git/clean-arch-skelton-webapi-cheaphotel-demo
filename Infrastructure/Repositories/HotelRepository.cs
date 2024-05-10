using Application.Interface;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
        }

        public async Task<List<Hotel>> GetHotelByNameAsync(string name)
        {
            return await _hotelDbContext.Hotels.
                Where(x => x.Name.Contains(name)).ToListAsync();
        }
    }
}
