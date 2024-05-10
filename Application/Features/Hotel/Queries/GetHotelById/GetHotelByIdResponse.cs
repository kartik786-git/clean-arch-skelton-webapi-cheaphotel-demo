using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Queries.GetHotelById
{
    public class GetHotelByIdResponse : BaseResponse
    {
        public HotelDto Hotel { get; set; }
    }
}
