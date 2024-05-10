using Application.Features.Room.Commands.CreateRoom;
using Application.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Room.Queries.GetRoomsAvailability;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateRoomResponse>> Create(RoomDto command)
        {
            var result = await Mediator.Send(new CreateRoomCommand() { CreateRoom = command });
            return result;
        }

        [HttpGet("RoomsAvailability")]
        public async Task<ActionResult<GetRoomsAvailabilityResponse>> GetHotelByName(string? name)
        {
            var result = await Mediator.Send(new GetRoomsAvailabilityQuery() { hotalName = name });
            return result;
        }
    }
}
