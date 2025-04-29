using Application.DTO.Receptionist;
using Application.Queries.ReceptionistQueries;
using AutoMapper;
using Domain.Interfaces;
using Domain.Entities;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.ReceptionistTests;

public class GetReceptionistsQueryHandlerTests
{
    [Fact]
    public async Task HandleReturnsListOfReceptionists()
    {
        var mockRepo = new Mock<IReceptionistRepository>();
        var mockMapper = new Mock<IMapper>();

        var receptionists = new List<Receptionist> { new Receptionist { FirstName = CQTestCases.ReceptionistsFirstName } };
        var responseDtos = new List<ResponseReceptionistDto> { new ResponseReceptionistDto { FirstName = CQTestCases.ReceptionistsFirstName } };

        mockRepo.Setup(r => r.GetAllAsync(default)).ReturnsAsync(receptionists);
        mockMapper.Setup(m => m.Map<List<ResponseReceptionistDto>>(receptionists)).Returns(responseDtos);

        var handler = new GetReceptionistsQueryHandler(mockRepo.Object, mockMapper.Object);

        var result = await handler.Handle(new GetReceptionistsQuery(), default);

        Assert.Single(result);
        Assert.Equal(CQTestCases.ReceptionistsFirstName, result[0].FirstName);
    }
}