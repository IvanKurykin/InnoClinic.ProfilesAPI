using Application.Commands.DoctorCommands;
using Application.DTO.Doctor;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.DoctorTests;

public class CreateDoctorCommandHandlerTests
{
    [Fact]
    public async Task HandleValidRequestCreatesDoctor()
    {
        var mockRepo = new Mock<IDoctorRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockBlobService = new Mock<IBlobStorageService>();

        var dto = new RequestDoctorDto { FirstName = CQTestCases.DoctorsFirstName };
        var doctor = new Doctor { FirstName = CQTestCases.DoctorsFirstName, DateOfBirth = DateTime.MinValue };
        var responseDto = new ResponseDoctorDto { FirstName = CQTestCases.DoctorsFirstName };

        mockMapper.Setup(m => m.Map<Doctor>(dto)).Returns(doctor);
        mockMapper.Setup(m => m.Map<ResponseDoctorDto>(It.IsAny<Doctor>())).Returns(responseDto);
        mockRepo.Setup(r => r.CreateAsync(doctor, default)).ReturnsAsync(doctor);

        var handler = new CreateDoctorCommandHandler(mockRepo.Object, mockMapper.Object, mockBlobService.Object);

        var result = await handler.Handle(new CreateDoctorCommand(dto), default);

        Assert.Equal(CQTestCases.DoctorsFirstName, result.FirstName);
        mockRepo.Verify(r => r.CreateAsync(doctor, default), Times.Once);
    }
}