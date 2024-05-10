using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Room.Queries.GetRoomsAvailability
{
    public class GetRoomsAvailabilityResponse : BaseResponse
    {
        public List<RoomsAvailabilityDto> RoomsAvailability { get; set; }
    }
}
