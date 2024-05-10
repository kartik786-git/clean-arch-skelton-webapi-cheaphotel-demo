using Application.Features.Hotel.Commands.CreateHotel;
using Application.Features;
using Application.Features.Hotel.Queries.GetHotelById;
using Application.Features.Hotel.Queries.GetHotelByName;
using Application.Features.Hotel.Queries.GetHotels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Hotel.Commands.UpdateHodel;
using Application.Features.Hotel.Commands.DeleteHotel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ApiControllerBase
    {
        [HttpGet]
        public async Task<GetHotelsResponse> GetHotels()
        {
            var response = await Mediator.Send(new GetHotelsQuery());
            return response;
        }


        [HttpGet("ByName{name}")]
        public async Task<ActionResult<GetHotelByNameResponse>> GetHotelByName(string name)
        {
            var result = await Mediator.Send(new GetHotelByNameQuery() { Name = name });
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelByIdResponse>> GetById(int id)
        {
            var result = await Mediator.Send(new GetHotelByIdQuery() { Id = id });
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<CreateHotelResponse>> Create(HotelDto command)
        {
            var result = await Mediator.Send(new CreateHotelCommand() { CreateHotel = command });
            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateHotelResponse>> Update(int id, HotelDto updatedBlog)
        {
            var result = await Mediator.Send(new UpdateHotelCommand() { Id = id, UpdateHotel = updatedBlog });

            return result;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteHotelReponse>> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteHotelCommand { Id = id });

            return result;
        }
    }
}
