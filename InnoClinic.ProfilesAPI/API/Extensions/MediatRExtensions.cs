using System.Diagnostics.CodeAnalysis;
using Application.Commands.DoctorCommands;

namespace API.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatRExtensions
{
    public static IServiceCollection AddCustomMetiatR(this IServiceCollection services)
    {
        services.AddMediatR(ctg => ctg.RegisterServicesFromAssembly(typeof(CreateDoctorCommandHandler).Assembly));

        return services;
    }
}