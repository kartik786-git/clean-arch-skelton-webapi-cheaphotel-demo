using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Hotel.Queries.GetHotels
{
    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, GetHotelsResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper, 
            ILogger<GetHotelsQueryHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetHotelsResponse> Handle(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            var getHotelResponse = new GetHotelsResponse();
            var validator = new GetHotelsValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    getHotelResponse.Success = false;
                    getHotelResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        getHotelResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (getHotelResponse.Success)
                {
                    var result = await _hotelRepository.GetAllAsync();
                    getHotelResponse.Hotels = _mapper.Map<List<HotelDto>>(result);
                }


            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                getHotelResponse.Success = false;
                // conver to you own message to show user
                getHotelResponse.Message = ex.Message;
            }
            return getHotelResponse;
        }
    }
}
