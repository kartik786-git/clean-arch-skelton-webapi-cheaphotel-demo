using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Queries.GetHotelByName
{
    public class GetHotelByNameQuery  : IRequest<GetHotelByNameResponse>
    {
        public string Name { get; set; }
    }
}
