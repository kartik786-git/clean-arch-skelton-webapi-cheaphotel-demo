using Application.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Commands.DeleteHotel
{
    public class DeleteHotelValidtor : AbstractValidator<DeleteHotelCommand>
    {
        private readonly IHotelRepository _hotelRepository;

        public DeleteHotelValidtor(IHotelRepository hotelRepository)
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("{PropertyName} greater then 0");
            RuleFor(x => x.Id).MustAsync(isExistBlog).WithMessage("{PropertyName} does not exits.");
            _hotelRepository = hotelRepository;
        }
        private async Task<bool> isExistBlog(int hotelId, CancellationToken cancellationToken)
        {
            var blog = await _hotelRepository.GetByIdAsync(hotelId);
            return blog?.Id > 0;
        }
    }
}
