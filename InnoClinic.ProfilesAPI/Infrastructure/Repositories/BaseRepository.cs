using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected ApplicationDbContext context;

    public BaseRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<T> CreateEntityAsync(T entity, CancellationToken cancellationToken = default)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteEntityAsync(T entity, CancellationToken cancellationToken = default)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<T>> GetAllEntitiesAsync(CancellationToken cancellationToken = default) =>
         await context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
       await context.Set<T>().FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, "Id") == id, cancellationToken);

    public async Task<T> UpdateEntityAsync(T entity, CancellationToken cancellationToken = default)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}