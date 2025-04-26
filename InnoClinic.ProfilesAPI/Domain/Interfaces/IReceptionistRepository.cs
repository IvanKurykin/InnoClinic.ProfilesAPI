using Domain.Entities;

namespace Domain.Interfaces;

public interface IReceptionistRepository
{
    Task<Receptionist> CreateReceptionistAsync(Receptionist receptionist, CancellationToken cancellationToken = default);
    Task<Receptionist> UpdateReceptionistAsync(Receptionist receptionist, CancellationToken cancellationToken = default);
    Task DeleteReceptionistAsync(Receptionist receptionist, CancellationToken cancellationToken = default);
    Task<List<Receptionist>> GetReceptionistsAsync(CancellationToken cancellationToken = default);
    Task<Receptionist?> GetReceptionistByIdAsync(Guid id, CancellationToken cancellationToken = default);
}