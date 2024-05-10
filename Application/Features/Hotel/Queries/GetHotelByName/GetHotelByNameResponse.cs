using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Queries.GetHotelByName
{
    public class GetHotelByNameResponse : BaseResponse
    {
        public List<HotelDto> Hotels { get; set; }
    }
}
