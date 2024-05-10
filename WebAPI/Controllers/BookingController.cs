using Application.Features.Booking.Commands.CreateBooking;
using Application.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateBookingResponse>> Create(BookingDto command)
        {
            var result = await Mediator.Send(new CreateBookingCommand() { CreateBooking = command });
            return result;
        }
    }
}
