using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Commands.CreateHotel
{
    public class CreateHotelResponse : BaseResponse
    {
        public int HotelId { get; set; }
    }
}
