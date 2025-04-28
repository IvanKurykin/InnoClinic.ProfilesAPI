using System.Diagnostics.CodeAnalysis;
using Application.Extensions;
using Application.Interfaces;
using Azure.Storage.Blobs;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton(x =>
            new BlobServiceClient(configuration["AzureStorage:ConnectionString"]));


        services.AddScoped<IBlobStorageService, BlobStorageService>();

        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();

        services.AddApplicationLayerServices();

        return services;
    }
}