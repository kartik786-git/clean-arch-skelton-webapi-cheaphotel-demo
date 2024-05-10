using Application.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Commands.UpdateHodel
{
    public class UpdateHotelValidator : AbstractValidator<UpdateHotelCommand>
    {
        private readonly IHotelRepository _hotelRepository;

        public UpdateHotelValidator(IHotelRepository hotelRepository)
        {
            RuleFor(p => p.UpdateHotel.Name)
         .NotEmpty()
         .NotNull()
         .WithMessage("{PropertyName} should have value");

            RuleFor(p => p.UpdateHotel.Address)
              .NotEmpty()
                .NotNull()
              .WithMessage("{PropertyName} should have value");

            RuleFor(x => x.Id).MustAsync(isExistBlog)
               .WithMessage("{PropertyName} does not exits.");

            _hotelRepository = hotelRepository;
        }

        private async Task<bool> isExistBlog(int hotelId, 
            CancellationToken cancellationToken)
        {
            var blog = await _hotelRepository.GetByIdAsync(hotelId).ConfigureAwait(false);
            return blog?.Id > 0;
        }
    }
}
