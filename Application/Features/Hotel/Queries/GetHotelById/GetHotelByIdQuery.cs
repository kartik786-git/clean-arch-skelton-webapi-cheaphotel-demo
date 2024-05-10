using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Queries.GetHotelById
{
    public class GetHotelByIdQuery : BaseCommandQuery, IRequest<GetHotelByIdResponse>
    {
    }
}
