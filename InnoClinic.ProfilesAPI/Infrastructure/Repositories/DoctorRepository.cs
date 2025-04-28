using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class DoctorRepository(ApplicationDbContext context) : BaseRepository<Doctor>(context), IDoctorRepository
{ }