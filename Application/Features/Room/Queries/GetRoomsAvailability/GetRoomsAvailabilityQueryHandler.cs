using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Room.Queries.GetRoomsAvailability
{
    public class GetRoomsAvailabilityQueryHandler : IRequestHandler<GetRoomsAvailabilityQuery, GetRoomsAvailabilityResponse>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetRoomsAvailabilityQueryHandler(IRoomRepository roomRepository,
            IMapper mapper,
            ILogger<GetRoomsAvailabilityQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetRoomsAvailabilityResponse> Handle(GetRoomsAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var hotelResponse = new GetRoomsAvailabilityResponse();
            var validator = new GetRoomsAvailabilityValidator();
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
                    var result = await _roomRepository.GetRoomsAvailability(request.hotalName);
                    hotelResponse.RoomsAvailability = _mapper.Map<List<RoomsAvailabilityDto>>(result);
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
