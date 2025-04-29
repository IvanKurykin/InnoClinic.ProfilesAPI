using API.Controller;
using Application.Commands.ReceptionistCommands;
using Application.DTO.Receptionist;
using Application.Queries.ReceptionistQueries;
using FluentAssertions;
using Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.Controllers;
public class ReceptionistControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ReceptionistController _controller;

    public ReceptionistControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ReceptionistController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateReceptionistAsyncReturnsOkResult()
    {
        var request = new RequestReceptionistDto();
        var expected = new ResponseReceptionistDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateReceptionistCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.CreateReceptionistAsync(request, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().Be(expected);
    }

    [Fact]
    public async Task UpdateReceptionistAsyncReturnsUpdatedReceptionist()
    {
        var id = Guid.NewGuid();
        var request = new RequestReceptionistDto();
        var expected = new ResponseReceptionistDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateReceptionistCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.UpdateReceptionistAsync(id, request, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().Be(expected);
    }

    [Fact]
    public async Task DeleteReceptionistAsyncReturnsSuccessMessage()
    {
        var id = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteReceptionistCommand>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _controller.DeleteReceptionistAsync(id, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        okResult.Value.Should().Be(Messages.ReceptionistDeletedSuccessfullyMessage);
    }

    [Fact]
    public async Task GetReceptionistByIdAsyncReturnsReceptionist()
    {
        var id = Guid.NewGuid();
        var expected = new ResponseReceptionistDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetReceptionistByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.GetReceptionistByIdAsync(id, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().Be(expected);
    }

    [Fact]
    public async Task GetReceptionistsAsyncReturnsListOfReceptionists()
    {
        var expected = new List<ResponseReceptionistDto> { new() };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetReceptionistsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.GetReceptionistsAsync(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().BeEquivalentTo(expected);
    }
}