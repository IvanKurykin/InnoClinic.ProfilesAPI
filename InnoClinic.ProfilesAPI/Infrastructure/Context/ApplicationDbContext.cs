using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Doctor>? Doctors { get; set; }
    public DbSet<Patient>? Patients { get; set; }
    public DbSet<Receptionist>? Receptionists { get; set; }
}