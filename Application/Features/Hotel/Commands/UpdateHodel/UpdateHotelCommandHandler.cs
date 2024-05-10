using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotel.Commands.UpdateHodel
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, UpdateHotelResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository,
            IMapper mapper,
            ILogger<UpdateHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<UpdateHotelResponse> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var updateBlogResponse = new UpdateHotelResponse();
            var validator = new UpdateHotelValidator(_hotelRepository);
            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    updateBlogResponse.Success = false;
                    updateBlogResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        updateBlogResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (updateBlogResponse.Success)
                {
                    var hotelEntity = await _hotelRepository.GetByIdAsync(request.Id);
                    _mapper.Map(request.UpdateHotel, hotelEntity);
                    await _hotelRepository.UpdateAsync(hotelEntity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while due to error- {ex.Message} ");
                updateBlogResponse.Success = false;
                updateBlogResponse.Message = ex.Message;
            }

            return updateBlogResponse;
        }
    }
}
