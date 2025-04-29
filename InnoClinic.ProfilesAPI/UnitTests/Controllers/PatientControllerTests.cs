using API.Controllers;
using Application.Commands.PatientCommands;
using Application.DTO.Patient;
using Application.Queries.PatientQueries;
using FluentAssertions;
using Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.Controllers;

public class PatientControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly PatientController _controller;

    public PatientControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new PatientController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreatePatientAsyncReturnsOkResult()
    {
        var request = new RequestPatientDto();
        var expected = new ResponsePatientDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePatientCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.CreatePatientAsync(request, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().Be(expected);
    }

    [Fact]
    public async Task UpdatePatientAsyncReturnsUpdatedPatient()
    {
        var id = Guid.NewGuid();
        var request = new RequestPatientDto();
        var expected = new ResponsePatientDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePatientCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.UpdatePatientAsync(id, request, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().Be(expected);
    }

    [Fact]
    public async Task DeletePatientAsyncReturnsSuccessMessage()
    {
        var id = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePatientCommand>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _controller.DeletePatientAsync(id, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        okResult.Value.Should().Be(Messages.PatientDeletedSuccessfullyMessage);
    }

    [Fact]
    public async Task GetPatientByIdAsyncReturnsPatient()
    {
        var id = Guid.NewGuid();
        var expected = new ResponsePatientDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPatientByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.GetPatientByIdAsync(id, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().Be(expected);
    }

    [Fact]
    public async Task GetPatientsAsyncReturnsListOfPatients()
    {
        var expected = new List<ResponsePatientDto> { new() };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPatientsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        var result = await _controller.GetPatientsAsync(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        okResult.Value.Should().BeEquivalentTo(expected);
    }
}