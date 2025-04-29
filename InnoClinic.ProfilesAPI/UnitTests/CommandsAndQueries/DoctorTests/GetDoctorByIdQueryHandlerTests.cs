using Application.DTO.Doctor;
using Application.Queries.DoctorQueries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.DoctorTests;

public class GetDoctorByIdQueryHandlerTests
{
    [Fact]
    public async Task HandleValidRequestReturnsDoctor()
    {
        var mockRepo = new Mock<IDoctorRepository>();
        var mockMapper = new Mock<IMapper>();

        var doctor = new Doctor { Id = Guid.NewGuid(), FirstName = CQTestCases.DoctorsFirstName, DateOfBirth = DateTime.MinValue};
        var responseDto = new ResponseDoctorDto { FirstName = CQTestCases.DoctorsFirstName };

        mockRepo.Setup(r => r.GetByIdAsync(doctor.Id, default)).ReturnsAsync(doctor);
        mockMapper.Setup(m => m.Map<ResponseDoctorDto>(doctor)).Returns(responseDto);

        var handler = new GetDoctorByIdQueryHandler(mockRepo.Object, mockMapper.Object);

        var result = await handler.Handle(new GetDoctorByIdQuery(doctor.Id), default);

        Assert.Equal(CQTestCases.DoctorsFirstName, result.FirstName);
    }
}