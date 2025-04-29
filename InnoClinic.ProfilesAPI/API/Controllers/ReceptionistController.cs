using Application.Commands.ReceptionistCommands;
using Application.DTO.Receptionist;
using Application.Queries.ReceptionistQueries;
using Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/[controller]")]
public class ReceptionistController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<ResponseReceptionistDto>> CreateReceptionistAsync([FromForm] RequestReceptionistDto dto, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new CreateReceptionistCommand(dto), cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseReceptionistDto>> UpdateReceptionistAsync([FromRoute] Guid id, [FromForm] RequestReceptionistDto dto, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new UpdateReceptionistCommand(id, dto), cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReceptionistAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteReceptionistCommand(id), cancellationToken);
        return Ok(Messages.ReceptionistDeletedSuccessfullyMessage);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseReceptionistDto>> GetReceptionistByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetReceptionistByIdQuery(id), cancellationToken));

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<ResponseReceptionistDto>>> GetReceptionistsAsync(CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetReceptionistsQuery(), cancellationToken));
}