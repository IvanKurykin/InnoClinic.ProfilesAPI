using Application.Commands.DoctorCommands;
using Application.DTO.Doctor;
using Application.Queries.DoctorQueries;
using Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<ResponseDoctorDto>> CreateDoctorAsync([FromForm] RequestDoctorDto dto, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new CreateDoctorCommand(dto), cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDoctorDto>> UpdateDoctorAsync([FromRoute] Guid id, [FromForm] RequestDoctorDto dto, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new UpdateDoctorCommand(id, dto), cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDoctorAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteDoctorCommand(id), cancellationToken);
        return Ok(Messages.DoctorDeletedSuccessfullyMessage);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDoctorDto>> GetDoctorByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetDoctorByIdQuery(id), cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<ResponseDoctorDto>>> GetDoctorsAsync(CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetDoctorsQuery(), cancellationToken));
}