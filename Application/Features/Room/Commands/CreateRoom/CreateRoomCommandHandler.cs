using Application.Features.Hotel.Commands.CreateHotel;
using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Room.Commands.CreateRoom
{
    internal class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, CreateRoomResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateRoomCommandHandler(IHotelRepository hotelRepository,
            IRoomRepository roomRepository,
            IMapper mapper,
            ILogger<CreateHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CreateRoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var createRoomResponse = new CreateRoomResponse();
            var validator = new CreateRoomValidator(_hotelRepository);

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    createRoomResponse.Success = false;
                    createRoomResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createRoomResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (createRoomResponse.Success)
                {
                    var hotelEntity = _mapper.Map<Domain.Entity.Room>(request.CreateRoom);
                    var result = await _roomRepository.AddAsync(hotelEntity);
                    createRoomResponse.RoomId = result.Id;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                createRoomResponse.Success = false;
                createRoomResponse.Message = ex.Message;

            }

            return createRoomResponse;
        }
    }
}
