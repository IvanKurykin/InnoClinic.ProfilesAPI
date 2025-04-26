using System.Numerics;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PatientRepository(ApplicationDbContext context) : IPatientRepository
{
    public async Task<Patient> CreatePatientAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        await context.Patients.AddAsync(patient, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public async Task DeletePatientAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        context.Patients.Remove(patient);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Patient?> GetPatientByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Patients.FindAsync(id, cancellationToken);

    public async Task<List<Patient>> GetPatientsAsync(CancellationToken cancellationToken = default) =>
        await context.Patients.ToListAsync(cancellationToken);

    public async Task<Patient> UpdatePatientAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        context.Patients.Update(patient);
        await context.SaveChangesAsync(cancellationToken);
        return patient;
    }
}