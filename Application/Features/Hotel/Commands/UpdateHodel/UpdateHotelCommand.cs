using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Commands.UpdateHodel
{
    public class UpdateHotelCommand  : BaseCommandQuery , IRequest<UpdateHotelResponse>
    {
        public HotelDto UpdateHotel { get; set; }
    }
}
