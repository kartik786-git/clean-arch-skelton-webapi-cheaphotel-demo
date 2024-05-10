using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetRoomsAvailability(string searchTerm);
    }
}
