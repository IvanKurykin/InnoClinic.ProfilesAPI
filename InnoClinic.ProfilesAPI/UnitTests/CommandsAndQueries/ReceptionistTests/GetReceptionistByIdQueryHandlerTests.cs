using Application.DTO.Receptionist;
using Application.Queries.ReceptionistQueries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.ReceptionistTests;

public class GetReceptionistByIdQueryHandlerTests
{
    [Fact]
    public async Task HandleValidRequestReturnsReceptionist()
    {
        var mockRepo = new Mock<IReceptionistRepository>();
        var mockMapper = new Mock<IMapper>();

        var receptionist = new Receptionist { Id = Guid.NewGuid(), FirstName = CQTestCases.ReceptionistsFirstName };
        var responseDto = new ResponseReceptionistDto { FirstName = CQTestCases.ReceptionistsFirstName };

        mockRepo.Setup(r => r.GetByIdAsync(receptionist.Id, default)).ReturnsAsync(receptionist);
        mockMapper.Setup(m => m.Map<ResponseReceptionistDto>(receptionist)).Returns(responseDto);

        var handler = new GetReceptionistByIdQueryHandler(mockRepo.Object, mockMapper.Object);

        var result = await handler.Handle(new GetReceptionistByIdQuery(receptionist.Id), default);

        Assert.Equal(CQTestCases.ReceptionistsFirstName, result.FirstName);
    }
}