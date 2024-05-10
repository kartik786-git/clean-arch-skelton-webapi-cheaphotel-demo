using Application.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Booking.Commands.CreateBooking
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        private readonly IRoomRepository _roomRepository;

        public CreateBookingValidator(IRoomRepository roomRepository)
        {
            RuleFor(p => p.CreateBooking.RoomId)
            .GreaterThan(0)
            .WithMessage("{PropertyName} should have value greater then 0");
            RuleFor(p => p.CreateBooking.CheckIn)
                 .LessThanOrEqualTo(DateTime.Now)
                  .WithMessage("{PropertyName} should have date greater then date");
            RuleFor(p => p.CreateBooking.CheckOut)
                .GreaterThan(DateTime.Now)
                 .WithMessage("{PropertyName} should have date greater then current date + 1");
            RuleFor(x => x.CreateBooking.RoomId).MustAsync(isExistBlog)
             .WithMessage("{PropertyName} does not exits.");
            _roomRepository = roomRepository;
        }
        private async Task<bool> isExistBlog(int roomid, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(roomid).ConfigureAwait(false);
            return room?.Id > 0;
        }

    }
}
