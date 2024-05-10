using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Commands.CreateHotel
{
    public class CreateHotelValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelValidator()
        {
            RuleFor(p => p.CreateHotel.Name)
              .NotEmpty()
              .NotNull()
              .WithMessage("{PropertyName} should have value");
            RuleFor(p => p.CreateHotel.Address)
              .NotEmpty()
                .NotNull()
              .WithMessage("{PropertyName} should have value");
        }
    }
}
