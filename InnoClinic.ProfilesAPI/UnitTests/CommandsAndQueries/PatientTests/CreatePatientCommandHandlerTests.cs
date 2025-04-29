using Application.Commands.PatientCommands;
using Application.DTO.Patient;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.PatientTests;

public class CreatePatientCommandHandlerTests
{
    [Fact]
    public async Task HandleValidRequestCreatesPatient()
    {
        var mockRepo = new Mock<IPatientRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockBlobService = new Mock<IBlobStorageService>();

        var dto = new RequestPatientDto { FirstName = CQTestCases.PatientsFirstName };
        var patient = new Patient { FirstName = CQTestCases.PatientsFirstName, DateOfBirth = DateTime.MinValue };
        var responseDto = new ResponsePatientDto { FirstName = CQTestCases.PatientsFirstName };

        mockMapper.Setup(m => m.Map<Patient>(dto)).Returns(patient);
        mockMapper.Setup(m => m.Map<ResponsePatientDto>(It.IsAny<Patient>())).Returns(responseDto);
        mockRepo.Setup(r => r.CreateAsync(patient, default)).ReturnsAsync(patient);

        var handler = new CreatePatientCommandHandler(mockRepo.Object, mockMapper.Object, mockBlobService.Object);

        var result = await handler.Handle(new CreatePatientCommand(dto), default);

        Assert.Equal(CQTestCases.PatientsFirstName, result.FirstName);
        mockRepo.Verify(r => r.CreateAsync(patient, default), Times.Once);
    }
}
