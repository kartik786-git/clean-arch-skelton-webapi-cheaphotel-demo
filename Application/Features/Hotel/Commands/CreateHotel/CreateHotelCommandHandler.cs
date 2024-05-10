using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Commands.CreateHotel
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, CreateHotelResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateHotelCommandHandler(IHotelRepository hotelRepository,
            IMapper mapper,
            ILogger<CreateHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CreateHotelResponse> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var createHotelResponse = new CreateHotelResponse();
            var validator = new CreateHotelValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    createHotelResponse.Success = false;
                    createHotelResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createHotelResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (createHotelResponse.Success)
                {
                    var hotelEntity = _mapper.Map<Domain.Entity.Hotel>(request.CreateHotel);
                    var result = await _hotelRepository.AddAsync(hotelEntity);
                    createHotelResponse.HotelId = result.Id;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                createHotelResponse.Success = false;
                createHotelResponse.Message = ex.Message;

            }

            return createHotelResponse;
        }
    }
}
