using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReceptionistRepository(ApplicationDbContext context) : IReceptionistRepository
{
    public async Task<Receptionist> CreateAsync(Receptionist receptionist, CancellationToken cancellationToken = default)
    {
        await context.Receptionists.AddAsync(receptionist, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return receptionist;
    }

    public async Task DeleteAsync(Receptionist receptionist, CancellationToken cancellationToken = default)
    {
        context.Receptionists.Remove(receptionist);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Receptionist?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Receptionists.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<List<Receptionist>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await context.Receptionists.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Receptionist> UpdateAsync(Receptionist receptionist, CancellationToken cancellationToken = default)
    {
        context.Receptionists.Update(receptionist);
        await context.SaveChangesAsync(cancellationToken);
        return receptionist;
    }
}