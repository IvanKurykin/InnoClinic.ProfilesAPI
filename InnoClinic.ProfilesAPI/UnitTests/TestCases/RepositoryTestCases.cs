using Domain.Entities;

namespace UnitTests.TestCases;

public static class RepositoryTestCases
{
    public static Patient GetTestPatient() => new()
    {
        Id = Guid.NewGuid(),
        FirstName = "TestPatient",
        LastName = "LastName",
        DateOfBirth = DateTime.MinValue
    };

    public static Doctor GetTestDoctor() => new()
    {
        Id = Guid.NewGuid(),
        FirstName = "TestDoctor",
        LastName = "LastName",
        Specialization = "General",
        DateOfBirth = DateTime.MinValue
    };

    public static Receptionist GetTestReceptionist() => new()
    {
        Id = Guid.NewGuid(),
        FirstName = "TestReceptionist",
        LastName = "LastName"
    };
}