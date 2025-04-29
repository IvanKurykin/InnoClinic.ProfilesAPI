using Application.Commands.ReceptionistCommands;
using Application.Interfaces;
using Domain.Interfaces;
using Moq;

namespace UnitTests.CommandsAndQueries.ReceptionistTests;

public class DeleteReceptionistCommandHandlerTests
{
    [Fact]
    public async Task HandleValidRequestDeletesReceptionist()
    {
        var mockRepo = new Mock<IReceptionistRepository>();
        var mockBlobService = new Mock<IBlobStorageService>();

        var receptionist = new Domain.Entities.Receptionist { Id = Guid.NewGuid() };
        mockRepo.Setup(r => r.GetByIdAsync(receptionist.Id, default)).ReturnsAsync(receptionist);

        var handler = new DeleteReceptionistCommandHandler(mockRepo.Object, mockBlobService.Object);

        await handler.Handle(new DeleteReceptionistCommand(receptionist.Id), default);

        mockRepo.Verify(r => r.DeleteAsync(receptionist, default), Times.Once);
    }
}