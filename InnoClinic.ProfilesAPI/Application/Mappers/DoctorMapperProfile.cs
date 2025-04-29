using System.Diagnostics.CodeAnalysis;
using Application.DTO.Doctor;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

[ExcludeFromCodeCoverage]
public class DoctorMapperProfile : Profile
{
    public DoctorMapperProfile()
    {
        CreateMap<RequestDoctorDto, Doctor>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());

        CreateMap<Doctor, ResponseDoctorDto>();
    }
}