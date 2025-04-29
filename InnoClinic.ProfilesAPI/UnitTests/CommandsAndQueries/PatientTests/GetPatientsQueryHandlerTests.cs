using Application.DTO.Patient;
using Application.Queries.PatientQueries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.PatientTests;

public class GetPatientsQueryHandlerTests
{
    [Fact]
    public async Task HandleReturnsListOfPatients()
    {
        var mockRepo = new Mock<IPatientRepository>();
        var mockMapper = new Mock<IMapper>();

        var patients = new List<Patient> { new Patient { FirstName = CQTestCases.PatientsFirstName, DateOfBirth = DateTime.MinValue } };
        var responseDtos = new List<ResponsePatientDto> { new ResponsePatientDto { FirstName = CQTestCases.PatientsFirstName } };

        mockRepo.Setup(r => r.GetAllAsync(default)).ReturnsAsync(patients);
        mockMapper.Setup(m => m.Map<List<ResponsePatientDto>>(patients)).Returns(responseDtos);

        var handler = new GetPatientsQueryHandler(mockRepo.Object, mockMapper.Object);

        var result = await handler.Handle(new GetPatientsQuery(), default);

        Assert.Single(result);
        Assert.Equal(CQTestCases.PatientsFirstName, result[0].FirstName);
    }
}