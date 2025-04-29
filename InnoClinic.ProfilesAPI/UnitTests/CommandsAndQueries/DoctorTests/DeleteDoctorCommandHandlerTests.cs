using Application.Commands.DoctorCommands;
using Application.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace UnitTests.CommandsAndQueries.DoctorTests;

public class DeleteDoctorCommandHandlerTests
{
    [Fact]
    public async Task HandleValidRequestDeletesDoctor()
    {
        var mockRepo = new Mock<IDoctorRepository>();
        var mockBlobService = new Mock<IBlobStorageService>();

        var doctor = new Doctor { Id = Guid.NewGuid(), DateOfBirth = DateTime.MinValue };
        mockRepo.Setup(r => r.GetByIdAsync(doctor.Id, default)).ReturnsAsync(doctor);

        var handler = new DeleteDoctorCommandHandler(mockRepo.Object, mockBlobService.Object);

        await handler.Handle(new DeleteDoctorCommand(doctor.Id), default);

        mockRepo.Verify(r => r.DeleteAsync(doctor, default), Times.Once);
    }
}