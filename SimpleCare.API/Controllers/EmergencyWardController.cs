using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCare.EmergencyWards.Application.Commands;
using SimpleCare.EmergencyWards.Application.Queries;
using SimpleCare.EmergencyWards.Application.Values;
using SimpleCare.EmergencyWards.Domain;

using System.Net.Mime;

namespace SimpleCare.API.Controllers;

[Route("api/emergency-wards")]
[ApiController]
[Authorize]
public class EmergencyWardController(IMediator mediator, ILogger<EmergencyWardController> logger) : ControllerBase
{
    [HttpGet("patients", Name = "GetEmergencyPatients")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<EmergencyPatientListItem[]>> GetPatients([FromQuery] EmergencyPatientStatus[] status, CancellationToken cancellationToken)
    {
        var query = new GetPatientsQuery(status);
        var result = await mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("patients/{patientId}", Name = "GetEmergencyPatient")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<EmergencyPatient>> GetPatient(Guid patientId, CancellationToken cancellationToken)
    {
        var query = new GetPatientQuery(patientId);
        var result = await mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("recipient-wards", Name = "GetRecipientWards")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<RecipientWardItem[]>> GetRecipientWards(CancellationToken cancellationToken)
    {
        var query = new GetRecipientWardsQuery();
        var result = await mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost("patients/register", Name = "RegisterEmergencyPatient")]
    public async Task<ActionResult> Register(EmergencyRegistration emergencyRegistration, CancellationToken cancellationToken)
    {
        var command = new RegisterPatientCommand(emergencyRegistration);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("patients/transfer", Name = "TransferEmergencyPatient")]
    public async Task<ActionResult> Transfer(TransferRequest transferRequest, CancellationToken cancellationToken)
    {
        var command = new TransferPatientCommand(transferRequest);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}
