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
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
        }

        public async Task<IEnumerable<Room>> GetRoomsAvailability(string searchTerm)
        {
            IQueryable<Room> query = _hotelDbContext.Set<Room>();

            query = query.Include(x => x.Bookings).Include(x => x.Hotel);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.
                    Where(p => EF.Functions.Like(p.Hotel.Name, $"%{searchTerm}%"));
            }
            return await query.ToListAsync();
        }
    }
}
