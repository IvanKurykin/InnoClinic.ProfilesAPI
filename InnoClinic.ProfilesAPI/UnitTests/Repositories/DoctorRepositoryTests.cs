using FluentAssertions;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestCases;

namespace UnitTests.Repositories;
public class DoctorRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly DoctorRepository _repository;

    public DoctorRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new DoctorRepository(_context);
    }

    [Fact]
    public async Task CreateAsyncShouldAddDoctor()
    {
        var doctor = RepositoryTestCases.GetTestDoctor();

        var result = await _repository.CreateAsync(doctor);

        result.Should().BeEquivalentTo(doctor);
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnDoctor()
    {
        var doctor = RepositoryTestCases.GetTestDoctor();

        await _context.Doctors!.AddAsync(doctor);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(doctor.Id);

        result.Should().BeEquivalentTo(doctor);
    }

    [Fact]
    public async Task DeleteAsyncShouldRemoveDoctor()
    {
        var doctor = RepositoryTestCases.GetTestDoctor();

        await _context.Doctors!.AddAsync(doctor);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(doctor);

        (await _context.Doctors.FindAsync(doctor.Id)).Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdateDoctor()
    {
        var doctor = RepositoryTestCases.GetTestDoctor();

        await _context.Doctors!.AddAsync(doctor);
        await _context.SaveChangesAsync();

        doctor.FirstName = "Updated Name";

        var updatedDoctor = await _repository.UpdateAsync(doctor);

        updatedDoctor.FirstName.Should().Be("Updated Name");
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllDoctors()
    {
        var doctor1 = RepositoryTestCases.GetTestDoctor();
        var doctor2 = RepositoryTestCases.GetTestDoctor();

        doctor2.Id = Guid.NewGuid();

        await _context.Doctors!.AddAsync(doctor1);
        await _context.Doctors.AddAsync(doctor2);
        await _context.SaveChangesAsync();

        var doctors = await _repository.GetAllAsync();

        doctors.Should().HaveCount(2);
    }
}