using Application.DTO.Doctor;
using Application.Queries.DoctorQueries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.DoctorTests;

public class GetDoctorsQueryHandlerTests
{
    [Fact]
    public async Task HandleReturnsListOfDoctors()
    {
        var mockRepo = new Mock<IDoctorRepository>();
        var mockMapper = new Mock<IMapper>();

        var doctors = new List<Doctor> { new Doctor { FirstName = CQTestCases.DoctorsFirstName, DateOfBirth = DateTime.MinValue} };
        var responseDtos = new List<ResponseDoctorDto> { new ResponseDoctorDto { FirstName = CQTestCases.DoctorsFirstName } };

        mockRepo.Setup(r => r.GetAllAsync(default)).ReturnsAsync(doctors);
        mockMapper.Setup(m => m.Map<List<ResponseDoctorDto>>(doctors)).Returns(responseDtos);

        var handler = new GetDoctorsQueryHandler(mockRepo.Object, mockMapper.Object);

        var result = await handler.Handle(new GetDoctorsQuery(), default);

        Assert.Single(result);
        Assert.Equal(CQTestCases.DoctorsFirstName, result[0].FirstName);
    }
}