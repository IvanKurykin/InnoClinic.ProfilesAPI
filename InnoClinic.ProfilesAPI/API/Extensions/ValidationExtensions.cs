using System.Diagnostics.CodeAnalysis;
using Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.Extensions;

[ExcludeFromCodeCoverage]
public static class ValidationExtensions
{
    public static IServiceCollection AddCustomValidation(this IServiceCollection services)
    {
        return services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<RequestDoctorDtoValidator>();
    }
}