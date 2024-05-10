using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Commands.CreateHotel
{
    public class CreateHotelCommand  : IRequest<CreateHotelResponse>
    {
        public HotelDto CreateHotel { get; set; }
    }
}
