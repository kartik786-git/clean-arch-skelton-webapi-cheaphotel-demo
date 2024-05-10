using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Room.Queries.GetRoomsAvailability
{
    public class RoomsAvailabilityDto : RoomDto
    {
        public string HotelName { get; set; }
        public bool IsRoomAvailable { get; set; }
        public double LeftDays { get; set; }
    }
}
