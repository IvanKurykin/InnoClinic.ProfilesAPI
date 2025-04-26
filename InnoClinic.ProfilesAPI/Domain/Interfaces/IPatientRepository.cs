using Domain.Entities;

namespace Domain.Interfaces;

public interface IPatientRepository
{
    Task<Patient> CreatePatientAsync(Patient patient, CancellationToken cancellationToken = default);
    Task<Patient> UpdatePatientAsync(Patient patient, CancellationToken cancellationToken = default);
    Task DeletePatientAsync(Patient patient, CancellationToken cancellationToken = default);
    Task<List<Patient>> GetPatientsAsync(CancellationToken cancellationToken = default);
    Task<Patient?> GetPatientByIdAsync(Guid id, CancellationToken cancellationToken = default);
}