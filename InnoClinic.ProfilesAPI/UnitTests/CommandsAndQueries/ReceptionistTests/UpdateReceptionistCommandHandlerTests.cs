using Application.Commands.ReceptionistCommands;
using Application.DTO.Receptionist;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Entities;
using Moq;
using UnitTests.TestCases;
using FluentAssertions;

namespace UnitTests.CommandsAndQueries.ReceptionistTests;

public class UpdateReceptionistCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithNewPhoto_UpdatesReceptionistPhoto()
    {
        var mockRepo = new Mock<IReceptionistRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockBlob = new Mock<IBlobStorageService>();

        var receptionistId = Guid.NewGuid();
        var formFile = CQTestCases.GetTestFormFile();
        var dto = new RequestReceptionistDto { Photo = formFile };
        var existing = new Receptionist { Id = receptionistId, PhotoUrl = CQTestCases.ReceptionistsOldPhotoUrl };
        var updated = new Receptionist { Id = receptionistId, PhotoUrl = CQTestCases.ReceptionistsNewPhotoUrl };
        var response = new ResponseReceptionistDto { PhotoUrl = CQTestCases.ReceptionistsNewPhotoUrl };

        mockRepo.Setup(r => r.GetByIdAsync(receptionistId, It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        mockBlob.Setup(b => b.UploadPhotoAsync(formFile)).ReturnsAsync(CQTestCases.ReceptionistsNewPhotoUrl);
        mockRepo.Setup(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>())).ReturnsAsync(updated);
        mockMapper.Setup(m => m.Map<ResponseReceptionistDto>(updated)).Returns(response);

        var handler = new UpdateReceptionistCommandHandler(mockRepo.Object, mockMapper.Object, mockBlob.Object);

        var result = await handler.Handle(new UpdateReceptionistCommand(receptionistId, dto), default);

        mockBlob.Verify(b => b.UploadPhotoAsync(formFile), Times.Once);
        mockBlob.Verify(b => b.DeletePhotoAsync(CQTestCases.ReceptionistsOldPhotoUrl), Times.Once);
        result.PhotoUrl.Should().Be(CQTestCases.ReceptionistsNewPhotoUrl);
    }
}