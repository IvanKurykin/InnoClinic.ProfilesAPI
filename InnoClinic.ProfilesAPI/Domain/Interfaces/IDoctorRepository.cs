using Domain.Entities;

namespace Domain.Interfaces;

public interface IDoctorRepository
{
    Task<Doctor> CreateDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task<Doctor> UpdateDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task DeleteDoctorAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task<List<Doctor>> GetDoctorsAsync(CancellationToken cancellationToken = default);
    Task<Doctor?> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken = default);
}