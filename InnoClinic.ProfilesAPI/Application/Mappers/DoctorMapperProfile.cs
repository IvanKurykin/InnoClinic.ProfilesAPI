using Application.DTO.Doctor;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class DoctorMapperProfile : Profile
{
    DoctorMapperProfile()
    {
        CreateMap<RequestDoctorDto, Doctor>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());

        CreateMap<Doctor, ResponseDoctorDto>();
    }
}