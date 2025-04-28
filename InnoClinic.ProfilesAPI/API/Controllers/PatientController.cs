using Application.Commands.PatientCommands;
using Application.DTO.Patient;
using Application.Queries.PatientQueries;
using Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<ResponsePatientDto>> CreatePatientAsync([FromForm] RequestPatientDto dto, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new CreatePatientCommand(dto), cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponsePatientDto>> UpdatePatientAsync([FromRoute] Guid id, [FromForm] RequestPatientDto dto, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new UpdatePatientCommand(id, dto), cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePatientAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeletePatientCommand(id), cancellationToken);
        return Ok(Messages.PatientDeletedSuccessfullyMessage);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponsePatientDto>> GetPatientByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetPatientByIdQuery(id), cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<ResponsePatientDto>>> GetPatientsAsync(CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetPatientsQuery(), cancellationToken));
}