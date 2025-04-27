using System.Numerics;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PatientRepository(ApplicationDbContext context) : IPatientRepository
{
    public async Task<Patient> CreateAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        await context.Patients.AddAsync(patient, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return patient;
    }

    public async Task DeleteAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        context.Patients.Remove(patient);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Patients.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Patients.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Patient> UpdateAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        context.Patients.Update(patient);
        await context.SaveChangesAsync(cancellationToken);
        return patient;
    }
}