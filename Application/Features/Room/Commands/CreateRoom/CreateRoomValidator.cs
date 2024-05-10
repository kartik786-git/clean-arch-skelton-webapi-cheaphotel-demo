using Application.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Room.Commands.CreateRoom
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomCommand>
    {
        private readonly IHotelRepository _hotelRepository;

        public CreateRoomValidator(IHotelRepository hotelRepository)
        {
            RuleFor(p => p.CreateRoom.Price)
              .GreaterThan(0)
              .WithMessage("{PropertyName} should have value greater then 0");
            RuleFor(p => p.CreateRoom.Capacity)
             .GreaterThan(0)
              .WithMessage("{PropertyName} should have value greater then 0");
            RuleFor(x => x.CreateRoom.HotelId).MustAsync(isExistBlog)
             .WithMessage("{PropertyName} does not exits.");
            _hotelRepository = hotelRepository;
        }
        private async Task<bool> isExistBlog(int hotelId, 
            CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(hotelId).ConfigureAwait(false);
            return hotel?.Id > 0;
        }
    }
}
