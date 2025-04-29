using Application.DTO.Receptionist;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.ReceptionistCommands;

public record CreateReceptionistCommand(RequestReceptionistDto Dto) : IRequest<ResponseReceptionistDto>;

public class CreateReceptionistCommandHandler(IReceptionistRepository receptionistRepository, IMapper mapper, IBlobStorageService blobStorageService) : IRequestHandler<CreateReceptionistCommand, ResponseReceptionistDto>
{
    public async Task<ResponseReceptionistDto> Handle(CreateReceptionistCommand request, CancellationToken cancellationToken)
    {
        if (request.Dto is null) throw new DtoIsNullException();

        var receptionist = mapper.Map<Receptionist>(request.Dto);

        if (request.Dto.Photo is not null)
        {
            var photoUrl = await blobStorageService.UploadPhotoAsync(request.Dto.Photo);
            receptionist.PhotoUrl = photoUrl;
        }

        var createdReceptionist = await receptionistRepository.CreateAsync(receptionist, cancellationToken);

        return mapper.Map<ResponseReceptionistDto>(createdReceptionist);
    }
}