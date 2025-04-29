using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class PatientRepository(ApplicationDbContext context) : BaseRepository<Patient>(context), IPatientRepository
{ }