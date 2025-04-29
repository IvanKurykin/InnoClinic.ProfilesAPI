using Application.Commands.ReceptionistCommands;
using Application.DTO.Receptionist;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Entities;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.CommandsAndQueries.ReceptionistTests;

public class UpdateReceptionistCommandHandlerTests
{
    [Fact]
    public async Task HandleValidRequestUpdatesReceptionist()
    {
        var mockRepo = new Mock<IReceptionistRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockBlobService = new Mock<IBlobStorageService>();

        var existingReceptionist = new Receptionist { Id = Guid.NewGuid(), FirstName = CQTestCases.ReceptionistsFirstName };
        var dto = new RequestReceptionistDto { FirstName = CQTestCases.ReceptionistsFirstName };
        var responseDto = new ResponseReceptionistDto { FirstName = CQTestCases.ReceptionistsFirstName };

        mockRepo.Setup(r => r.GetByIdAsync(existingReceptionist.Id, default)).ReturnsAsync(existingReceptionist);
        mockMapper.Setup(m => m.Map<ResponseReceptionistDto>(existingReceptionist)).Returns(responseDto);

        var handler = new UpdateReceptionistCommandHandler(mockRepo.Object, mockMapper.Object, mockBlobService.Object);

        var result = await handler.Handle(new UpdateReceptionistCommand(existingReceptionist.Id, dto), default);

        mockRepo.Verify(r => r.UpdateAsync(existingReceptionist, default), Times.Once);
    }
}