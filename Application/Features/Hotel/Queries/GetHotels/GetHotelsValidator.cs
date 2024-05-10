using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Queries.GetHotels
{
    public class GetHotelsValidator : AbstractValidator<GetHotelsQuery>
    {
        public GetHotelsValidator()
        {
            
        }
    }
}
