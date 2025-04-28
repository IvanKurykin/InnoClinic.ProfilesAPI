using Application.DTO.Receptionist;
using Application.Exceptions.NotFoundExceptions;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.ReceptionistQueries;

public record GetReceptionistByIdQuery(Guid Id) : IRequest<ResponseReceptionistDto>;

public class GetReceptionistByIdQueryHandler(IReceptionistRepository receptionistRepository, IMapper mapper) : IRequestHandler<GetReceptionistByIdQuery, ResponseReceptionistDto>
{
    public async Task<ResponseReceptionistDto> Handle(GetReceptionistByIdQuery request, CancellationToken cancellationToken)
    {
        var receptionist = await receptionistRepository.GetByIdAsync(request.Id, cancellationToken);

        if (receptionist is null) throw new ReceptionistNotFoundException();

        return mapper.Map<ResponseReceptionistDto>(receptionist);
    }
}