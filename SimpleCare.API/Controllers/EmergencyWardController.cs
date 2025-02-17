using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleCare.EmergencyWards.Application.Commands;
using SimpleCare.EmergencyWards.Application.Queries;
using SimpleCare.EmergencyWards.Application.Values;
using System.Net.Mime;

namespace SimpleCare.API.Controllers;

[Route("api/emergency-wards")]
[ApiController]
public class EmergencyWardController(IMediator mediator, ILogger<EmergencyWardController> logger) : ControllerBase
{
    [HttpGet("patients", Name = "GetPatients")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<EmergencyPatientListItem[]>> GetPatients(CancellationToken cancellationToken)
    {
        var query = new GetPatientsQuery();
        var result = await mediator.Send(query, cancellationToken);

        return Ok(result.ToArray());
    }

    [HttpGet("patients/{patientId}", Name = "GetPatient")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<EmergencyPatient>> GetPatient(Guid patientId, CancellationToken cancellationToken)
    {
        var query = new GetPatientQuery(patientId);
        var result = await mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost("register", Name = "Register")]
    public async Task<ActionResult> Register(EmergencyRegistration emergencyRegistration, CancellationToken cancellationToken)
    {
        var command = new RegisterPatientCommand(emergencyRegistration);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }


    [HttpPost("transfer", Name = "Transfer")]
    public async Task<ActionResult> Transfer(TransferRequest transferRequest, CancellationToken cancellationToken)
    {
        var command = new TransferPatientCommand(transferRequest);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}
