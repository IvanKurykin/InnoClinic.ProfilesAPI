using FluentAssertions;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestCases;

namespace UnitTests.Repositories;

public class PatientRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly PatientRepository _repository;

    public PatientRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        _context = new ApplicationDbContext(options);
        _repository = new PatientRepository(_context);
    }

    [Fact]
    public async Task CreateAsyncShouldAddPatient()
    {
        var patient = RepositoryTestCases.GetTestPatient();

        var result = await _repository.CreateAsync(patient);

        result.Should().BeEquivalentTo(patient);
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnPatient()
    {
        var patient = RepositoryTestCases.GetTestPatient();

        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(patient.Id);

        result.Should().BeEquivalentTo(patient);
    }

    [Fact]
    public async Task DeleteAsyncShouldRemovePatient()
    {
        var patient = RepositoryTestCases.GetTestPatient();

        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(patient);

        (await _context.Patients.FindAsync(patient.Id)).Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdatePatient()
    {
        var patient = RepositoryTestCases.GetTestPatient();

        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();

        patient.FirstName = "Updated Name";

        var updatedPatient = await _repository.UpdateAsync(patient);

        updatedPatient.FirstName.Should().Be("Updated Name");
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllPatients()
    {
        var patient1 = RepositoryTestCases.GetTestPatient();
        var patient2 = RepositoryTestCases.GetTestPatient();

        await _context.Patients.AddAsync(patient1);
        await _context.Patients.AddAsync(patient2);
        await _context.SaveChangesAsync();

        var patients = await _repository.GetAllAsync();

        patients.Should().HaveCount(2);
    }
}