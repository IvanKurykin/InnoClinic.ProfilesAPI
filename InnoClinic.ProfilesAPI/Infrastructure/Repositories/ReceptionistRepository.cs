using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class ReceptionistRepository : BaseRepository<Receptionist>, IReceptionistRepository
{
    public ReceptionistRepository(ApplicationDbContext context) : base(context) { }
}