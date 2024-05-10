using Application.Features.Hotel.Queries.GetHotels;
using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Queries.GetHotelByName
{
    public class GetHotelByNameQueryHandler : IRequestHandler<GetHotelByNameQuery, GetHotelByNameResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetHotelByNameQueryHandler(IHotelRepository hotelRepository, IMapper mapper,
            ILogger<GetHotelByNameQueryHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetHotelByNameResponse> Handle(GetHotelByNameQuery request, CancellationToken cancellationToken)
        {
            var hotelResponse = new GetHotelByNameResponse();
            var validator = new GetHotelByNameValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    hotelResponse.Success = false;
                    hotelResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        hotelResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (hotelResponse.Success)
                {
                    var result = await _hotelRepository.GetHotelByNameAsync(request.Name);
                    hotelResponse.Hotels = _mapper.Map<List<HotelDto>>(result);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                hotelResponse.Success = false;
                hotelResponse.Message = ex.Message;

            }

            return hotelResponse;
        }
    }
}
