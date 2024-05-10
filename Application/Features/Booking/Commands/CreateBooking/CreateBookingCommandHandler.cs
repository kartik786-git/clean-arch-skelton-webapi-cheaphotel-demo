using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Booking.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingResponse>
    {
        private readonly IRepository<Domain.Entity.Booking> _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreateBookingCommandHandler(IRepository<Domain.Entity.Booking> bookingRepository,
            IRoomRepository roomRepository,
            IMapper mapper,
            ILogger<CreateBookingCommandHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateBookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var createBookingResponse = new CreateBookingResponse();
            var validator = new CreateBookingValidator(_roomRepository);

            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    createBookingResponse.Success = false;
                    createBookingResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        createBookingResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (createBookingResponse.Success)
                {
                    var bookingEntity = _mapper.Map<Domain.Entity.Booking>(request.CreateBooking);
                    var result = await _bookingRepository.AddAsync(bookingEntity);
                    createBookingResponse.BookingId = result.Id;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"error while due to error- {ex.Message} ");
                createBookingResponse.Success = false;
                createBookingResponse.Message = ex.Message;

            }

            return createBookingResponse;
        }
    }
}
