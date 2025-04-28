namespace Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> CreateEntityAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateEntityAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteEntityAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllEntitiesAsync(CancellationToken cancellationToken = default);
    Task<T?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default);
}