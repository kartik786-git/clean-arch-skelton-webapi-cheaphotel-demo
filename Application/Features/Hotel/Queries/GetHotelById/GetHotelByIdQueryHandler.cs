using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Queries.GetHotelById
{
    public class GetHotelByIdQueryHandler  : IRequestHandler<GetHotelByIdQuery, GetHotelByIdResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetHotelByIdQueryHandler(IHotelRepository hotelRepository, IMapper mapper,
            ILogger<GetHotelByIdQueryHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetHotelByIdResponse> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            var hotelResponse = new GetHotelByIdResponse();
            var validator = new GetHotelByIdValidator();

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
                    var result = await _hotelRepository.GetByIdAsync(request.Id);
                    hotelResponse.Hotel = _mapper.Map<HotelDto>(result);
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
