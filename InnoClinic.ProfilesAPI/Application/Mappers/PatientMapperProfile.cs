﻿using System.Diagnostics.CodeAnalysis;
using Application.DTO.Patient;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

[ExcludeFromCodeCoverage]
public class PatientMapperProfile : Profile
{
    public PatientMapperProfile()
    {
        CreateMap<RequestPatientDto, Patient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());

        CreateMap<Patient, ResponsePatientDto>();
    }
}