using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Booking.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<CreateBookingResponse>
    {
        public BookingDto CreateBooking { get; set; }
    }
}
