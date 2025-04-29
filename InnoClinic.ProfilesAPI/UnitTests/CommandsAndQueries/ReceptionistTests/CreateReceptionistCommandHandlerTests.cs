using Application.Commands.ReceptionistCommands;
using Application.DTO.Receptionist;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.ReceptionistTests;

public class CreateReceptionistCommandHandlerTests
{
    [Fact]
    public async Task HandleValidRequestCreatesReceptionist()
    {
        var mockRepo = new Mock<IReceptionistRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockBlobService = new Mock<IBlobStorageService>();

        var dto = new RequestReceptionistDto { FirstName = CQTestCases.ReceptionistsFirstName };
        var receptionist = new Receptionist { FirstName = CQTestCases.ReceptionistsFirstName };
        var responseDto = new ResponseReceptionistDto { FirstName = CQTestCases.ReceptionistsFirstName };

        mockMapper.Setup(m => m.Map<Receptionist>(dto)).Returns(receptionist);
        mockMapper.Setup(m => m.Map<ResponseReceptionistDto>(It.IsAny<Receptionist>())).Returns(responseDto);
        mockRepo.Setup(r => r.CreateAsync(receptionist, default)).ReturnsAsync(receptionist);

        var handler = new CreateReceptionistCommandHandler(mockRepo.Object, mockMapper.Object, mockBlobService.Object);

        var result = await handler.Handle(new CreateReceptionistCommand(dto), default);

        Assert.Equal(CQTestCases.ReceptionistsFirstName, result.FirstName);
        mockRepo.Verify(r => r.CreateAsync(receptionist, default), Times.Once);
    }
}