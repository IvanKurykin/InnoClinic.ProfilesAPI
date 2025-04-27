using Application.DTO.Patient;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.PatientQueries;

public record GetPatientsQuery : IRequest<List<ResponsePatientDto>>;

public class GetPatientsQueryHandler(IPatientRepository patientRepository, IMapper mapper) : IRequestHandler<GetPatientsQuery, List<ResponsePatientDto>>
{
    public async Task<List<ResponsePatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        var patients = await patientRepository.GetPatientsAsync(cancellationToken);

        return mapper.Map<List<ResponsePatientDto>>(patients);
    }
}