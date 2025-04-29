using API.Controllers;
using Application.Commands.DoctorCommands;
using Application.DTO.Doctor;
using Application.Queries.DoctorQueries;
using FluentAssertions;
using Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.Controllers;

public class DoctorControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly DoctorController _controller;

    public DoctorControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new DoctorController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateDoctorAsyncReturnsOkResult()
    {
        var request = new RequestDoctorDto();
        var expected = new ResponseDoctorDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateDoctorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.CreateDoctorAsync(request, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().Be(expected);
    }

    [Fact]
    public async Task UpdateDoctorAsyncReturnsUpdatedDoctor()
    {
        var id = Guid.NewGuid();
        var request = new RequestDoctorDto();
        var expected = new ResponseDoctorDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateDoctorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.UpdateDoctorAsync(id, request, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().Be(expected);
    }

    [Fact]
    public async Task DeleteDoctorAsyncReturnsSuccessMessage()
    {
        var id = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteDoctorCommand>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _controller.DeleteDoctorAsync(id, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        okResult.Value.Should().Be(Messages.DoctorDeletedSuccessfullyMessage);
    }

    [Fact]
    public async Task GetDoctorByIdAsyncReturnsDoctor()
    {
        var id = Guid.NewGuid();
        var expected = new ResponseDoctorDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetDoctorByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.GetDoctorByIdAsync(id, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().Be(expected);
    }

    [Fact]
    public async Task GetDoctorsAsyncReturnsListOfDoctors()
    {
        var expected = new List<ResponseDoctorDto> { new() };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetDoctorsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.GetDoctorsAsync(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().BeEquivalentTo(expected);
    }
}
