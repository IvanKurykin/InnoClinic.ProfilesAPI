using Application.Commands.PatientCommands;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace UnitTests.CommandsAndQueries.PatientTests;

public class DeletePatientCommandHandlerTests
{
    [Fact]
    public async Task HandleValidRequestDeletesPatient()
    {
        var mockRepo = new Mock<IPatientRepository>();
        var mockBlobService = new Mock<IBlobStorageService>();

        var patient = new Patient { Id = Guid.NewGuid(), DateOfBirth = DateTime.MinValue };
        mockRepo.Setup(r => r.GetByIdAsync(patient.Id, default)).ReturnsAsync(patient);

        var handler = new DeletePatientCommandHandler(mockRepo.Object, mockBlobService.Object);

        await handler.Handle(new DeletePatientCommand(patient.Id), default);

        mockRepo.Verify(r => r.DeleteAsync(patient, default), Times.Once);
    }
}