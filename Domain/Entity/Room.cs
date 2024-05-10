using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
