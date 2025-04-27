using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DoctorRepository(ApplicationDbContext context) : IDoctorRepository
{
    public async Task<Doctor> CreateDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        await context.Doctors.AddAsync(doctor, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return doctor;
    }

    public async Task DeleteDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        context.Doctors.Remove(doctor);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Doctor?> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<List<Doctor>> GetDoctorsAsync(CancellationToken cancellationToken = default) =>
        await context.Doctors.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Doctor> UpdateDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        context.Doctors.Update(doctor);
        await context.SaveChangesAsync(cancellationToken);
        return doctor;
    }
}