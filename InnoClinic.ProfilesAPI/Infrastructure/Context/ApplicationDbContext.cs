using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public required DbSet<Doctor> Doctors { get; set; }
    public required DbSet<Patient> Patients { get; set; }
    public required DbSet<Receptionist> Receptionists { get; set; }
}