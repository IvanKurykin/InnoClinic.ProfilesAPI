using Application.DTO.Patient;
using Application.Queries.PatientQueries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.PatientTests;

public class GetPatientByIdQueryHandlerTests
{
    [Fact]
    public async Task HandleValidRequestReturnsPatient()
    {
        var mockRepo = new Mock<IPatientRepository>();
        var mockMapper = new Mock<IMapper>();

        var patient = new Patient { Id = Guid.NewGuid(), FirstName = CQTestCases.PatientsFirstName, DateOfBirth = DateTime.MinValue };
        var responseDto = new ResponsePatientDto { FirstName = CQTestCases.PatientsFirstName };

        mockRepo.Setup(r => r.GetByIdAsync(patient.Id, default)).ReturnsAsync(patient);
        mockMapper.Setup(m => m.Map<ResponsePatientDto>(patient)).Returns(responseDto);

        var handler = new GetPatientByIdQueryHandler(mockRepo.Object, mockMapper.Object);

        var result = await handler.Handle(new GetPatientByIdQuery(patient.Id), default);

        Assert.Equal(CQTestCases.PatientsFirstName, result.FirstName);
    }
}