using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<List<Hotel>> GetHotelByNameAsync(string name);
    }
}
