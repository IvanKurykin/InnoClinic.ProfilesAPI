using Application.DTO.Patient;
using Application.Exceptions.NotFoundExceptions;
using AutoMapper;
using Domain.Constants;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.PatientQueries;

public record GetPatientByIdQuery(Guid Id) : IRequest<ResponsePatientDto>;

public class GetPatientByIdQueryHandler(IPatientRepository patientRepository, IMapper mapper) : IRequestHandler<GetPatientByIdQuery, ResponsePatientDto>
{
    public async Task<ResponsePatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (patient is null) throw new PatientNotFoundException();

        return mapper.Map<ResponsePatientDto>(patient);
    }
}