using System.Diagnostics.CodeAnalysis;
using Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

[ExcludeFromCodeCoverage]
public static class ApplicationExtension
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DoctorMapperProfile));
        services.AddAutoMapper(typeof(PatientMapperProfile));
        services.AddAutoMapper(typeof(ReceptionistMapperProfile));

        return services;
    }
}