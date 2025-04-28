using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class ReceptionistRepository(ApplicationDbContext context) : BaseRepository<Receptionist>(context), IReceptionistRepository
{ }