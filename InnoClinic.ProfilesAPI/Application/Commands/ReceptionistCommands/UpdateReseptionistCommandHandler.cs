using Application.DTO.Receptionist;
using Application.Exceptions.NotFoundExceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.ReceptionistCommands;

public record UpdateReceptionistCommand(Guid Id, RequestReceptionistDto Dto) : IRequest<ResponseReceptionistDto>;

public class UpdateReceptionistCommandHandler(IReceptionistRepository receptionistRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<UpdateReceptionistCommand, ResponseReceptionistDto>
{
    public async Task<ResponseReceptionistDto> Handle(UpdateReceptionistCommand request, CancellationToken cancellationToken)
    {
        var existedReceptionist = await receptionistRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existedReceptionist is null) throw new ReceptionistNotFoundException();

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);
            existedReceptionist.PhotoUrl = photoUrl;  
        }

        mapper.Map(request.Dto, existedReceptionist);

        var updatedReceptionist = await receptionistRepository.UpdateAsync(existedReceptionist, cancellationToken);

        return mapper.Map<ResponseReceptionistDto>(updatedReceptionist);
    }
}