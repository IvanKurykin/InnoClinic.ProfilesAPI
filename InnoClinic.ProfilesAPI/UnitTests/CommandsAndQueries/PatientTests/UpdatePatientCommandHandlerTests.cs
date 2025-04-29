using Application.Commands.DoctorCommands;
using Application.Commands.PatientCommands;
using Application.DTO.Doctor;
using Application.DTO.Patient;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.PatientTests;

public class UpdatePatientCommandHandlerTests
{
    [Fact]
    public async Task HandleValidRequestUpdatesPatient()
    {
        var mockRepo = new Mock<IPatientRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockBlobService = new Mock<IBlobStorageService>();

        var existingPatient = new Patient { Id = Guid.NewGuid(), FirstName = CQTestCases.PatientsFirstName, DateOfBirth = DateTime.MinValue };
        var dto = new RequestPatientDto { FirstName = CQTestCases.PatientsFirstName };
        var responseDto = new ResponsePatientDto { FirstName = CQTestCases.PatientsFirstName };

        mockRepo.Setup(r => r.GetByIdAsync(existingPatient.Id, default)).ReturnsAsync(existingPatient);
        mockMapper.Setup(m => m.Map<ResponsePatientDto>(existingPatient)).Returns(responseDto);

        var handler = new UpdatePatientCommandHandler(mockRepo.Object, mockMapper.Object, mockBlobService.Object);

        var result = await handler.Handle(new UpdatePatientCommand(existingPatient.Id, dto), default);

        mockRepo.Verify(r => r.UpdateAsync(existingPatient, default), Times.Once);
    }
}