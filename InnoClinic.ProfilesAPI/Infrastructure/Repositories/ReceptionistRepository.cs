using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class ReceptionistRepository : BaseRepository<Receptionist>, IReceptionistRepository
{
    public ReceptionistRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Receptionist> CreateAsync(Receptionist receptionist, CancellationToken cancellationToken = default) =>
         await CreateEntityAsync(receptionist, cancellationToken);

    public async Task DeleteAsync(Receptionist receptionist, CancellationToken cancellationToken = default) =>
         await DeleteEntityAsync(receptionist, cancellationToken);

    public async Task<Receptionist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await GetEntityByIdAsync(id, cancellationToken);

    public async Task<List<Receptionist>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await GetAllEntitiesAsync(cancellationToken);

    public async Task<Receptionist> UpdateAsync(Receptionist receptionist, CancellationToken cancellationToken = default) =>
        await UpdateEntityAsync(receptionist, cancellationToken);
}