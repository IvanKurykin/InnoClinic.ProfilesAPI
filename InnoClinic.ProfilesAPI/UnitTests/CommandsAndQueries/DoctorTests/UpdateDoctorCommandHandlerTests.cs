using Application.Commands.DoctorCommands;
using Application.DTO.Doctor;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.DoctorTests;

public class UpdateDoctorCommandHandlerTests
{
    [Fact]
    public async Task HandleValidRequestUpdatesDoctor()
    {
        var mockRepo = new Mock<IDoctorRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockBlobService = new Mock<IBlobStorageService>();

        var existingDoctor = new Doctor { Id = Guid.NewGuid(), FirstName = CQTestCases.DoctorsFirstName, DateOfBirth = DateTime.MinValue};
        var dto = new RequestDoctorDto { FirstName = CQTestCases.DoctorsNewFirstName };
        var responseDto = new ResponseDoctorDto { FirstName = CQTestCases.DoctorsNewFirstName };

        mockRepo.Setup(r => r.GetByIdAsync(existingDoctor.Id, default)).ReturnsAsync(existingDoctor);
        mockMapper.Setup(m => m.Map<ResponseDoctorDto>(existingDoctor)).Returns(responseDto);

        var handler = new UpdateDoctorCommandHandler(mockRepo.Object, mockMapper.Object, mockBlobService.Object);

        var result = await handler.Handle(new UpdateDoctorCommand(existingDoctor.Id, dto), default);

        mockRepo.Verify(r => r.UpdateAsync(existingDoctor, default), Times.Once);
    }
}