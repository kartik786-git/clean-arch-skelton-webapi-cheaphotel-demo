using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Queries.GetHotelById
{
    public class GetHotelByIdValidator : AbstractValidator<GetHotelByIdQuery>
    {
        public GetHotelByIdValidator()
        {
            RuleFor(p => p.Id)
               .GreaterThan(0)
               .WithMessage("{PropertyName} greater then 0");
        }
    }
}
