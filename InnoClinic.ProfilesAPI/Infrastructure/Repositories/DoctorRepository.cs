using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Doctor> CreateAsync(Doctor doctor, CancellationToken cancellationToken = default) =>
        await CreateEntityAsync(doctor, cancellationToken);
    
    public async Task DeleteAsync(Doctor doctor, CancellationToken cancellationToken = default) =>
        await DeleteEntityAsync(doctor, cancellationToken);

    public async Task<Doctor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await GetEntityByIdAsync(id, cancellationToken);

    public async Task<List<Doctor>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await GetAllEntitiesAsync(cancellationToken);
    public async Task<Doctor> UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default) =>
        await UpdateEntityAsync(doctor, cancellationToken);
}