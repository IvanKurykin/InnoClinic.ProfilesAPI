using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    public PatientRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Patient> CreateAsync(Patient patient, CancellationToken cancellationToken = default) =>
        await CreateEntityAsync(patient, cancellationToken);

    public async Task DeleteAsync(Patient patient, CancellationToken cancellationToken = default) =>
         await DeleteEntityAsync(patient, cancellationToken);

    public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await GetEntityByIdAsync(id, cancellationToken);

    public async Task<List<Patient>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await GetAllEntitiesAsync(cancellationToken);

    public async Task<Patient> UpdateAsync(Patient patient, CancellationToken cancellationToken = default) =>
        await UpdateEntityAsync(patient, cancellationToken);
}