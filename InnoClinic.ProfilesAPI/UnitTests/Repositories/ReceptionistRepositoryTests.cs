using FluentAssertions;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestCases;
using Xunit;

namespace UnitTests.Repositories;

public class ReceptionistRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly ReceptionistRepository _repository;

    public ReceptionistRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ReceptionistRepository(_context);
    }

    [Fact]
    public async Task CreateAsyncShouldAddReceptionist()
    {
        var receptionist = RepositoryTestCases.GetTestReceptionist();

        var result = await _repository.CreateAsync(receptionist);

        result.Should().BeEquivalentTo(receptionist);
    }

    [Fact]
    public async Task GetByIdAsyncShouldReturnReceptionist()
    {
        var receptionist = RepositoryTestCases.GetTestReceptionist();

        await _context.Receptionists!.AddAsync(receptionist);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(receptionist.Id);

        result.Should().BeEquivalentTo(receptionist);
    }

    [Fact]
    public async Task DeleteAsyncShouldRemoveReceptionist()
    {
        var receptionist = RepositoryTestCases.GetTestReceptionist();

        await _context.Receptionists!.AddAsync(receptionist);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(receptionist);

        (await _context.Receptionists.FindAsync(receptionist.Id)).Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsyncShouldUpdateReceptionist()
    {
        var receptionist = RepositoryTestCases.GetTestReceptionist();

        await _context.Receptionists!.AddAsync(receptionist);
        await _context.SaveChangesAsync();

        receptionist.FirstName = "Updated Name";

        var updatedReceptionist = await _repository.UpdateAsync(receptionist);

        updatedReceptionist.FirstName.Should().Be("Updated Name");
    }

    [Fact]
    public async Task GetAllAsyncShouldReturnAllReceptionists()
    {
        var receptionist1 = RepositoryTestCases.GetTestReceptionist();
        var receptionist2 = RepositoryTestCases.GetTestReceptionist();

        receptionist2.Id = Guid.NewGuid();

        await _context.Receptionists!.AddAsync(receptionist1);
        await _context.Receptionists.AddAsync(receptionist2);
        await _context.SaveChangesAsync();

        var receptionists = await _repository.GetAllAsync();

        receptionists.Should().HaveCount(2);
    }
}