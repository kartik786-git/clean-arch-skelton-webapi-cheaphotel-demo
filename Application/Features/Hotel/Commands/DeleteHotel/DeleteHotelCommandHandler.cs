using Application.Interface;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Hotel.Commands.DeleteHotel
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, DeleteHotelReponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository,
            IMapper mapper,
            ILogger<DeleteHotelCommandHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<DeleteHotelReponse> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var deleteBlogResponse = new DeleteHotelReponse();
            var validator = new DeleteHotelValidtor(_hotelRepository);
            try
            {
                var validationResult = await validator.ValidateAsync(request, new CancellationToken());
                if (validationResult.Errors.Count > 0)
                {
                    deleteBlogResponse.Success = false;
                    deleteBlogResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors.Select(x => x.ErrorMessage))
                    {
                        deleteBlogResponse.ValidationErrors.Add(error);
                        _logger.LogError($"validation failed due to error- {error} ");
                    }
                }
                else if (deleteBlogResponse.Success)
                {
                    var hotelEntity = await _hotelRepository.GetByIdAsync(request.Id);
                    await _hotelRepository.DeleteAsync(hotelEntity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error while due to error- {ex.Message} ");
                deleteBlogResponse.Success = false;
                deleteBlogResponse.Message = ex.Message;
            }

            return deleteBlogResponse;
        }
    }
}
