using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleCare.BedWards.Application.Commands;
using SimpleCare.BedWards.Application.Queries;
using SimpleCare.BedWards.Application.Values;
using System.Net.Mime;

namespace SimpleCare.API.Controllers
{
    [Route("api/bed-ward")]
    [ApiController]
    public class BedWardController(IMediator mediator, ILogger<BedWardController> logger) : ControllerBase
    {
        [HttpGet("patients", Name = "GetBedWardPatients")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<BedWardPatientListItem[]>> GetPatients(CancellationToken cancellationToken)
        {
            var query = new GetPatientsQuery();
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("patients/{patientId}", Name = "GetBedWardPatient")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<BedWardPatient>> GetPatient(Guid patientId, CancellationToken cancellationToken)
        {
            var query = new GetPatientQuery(patientId);
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost("patients/admit", Name = "AdmitPatient")]
        public async Task<ActionResult> Admit(PatientAdmission admission, CancellationToken cancellationToken)
        {
            var command = new AdmitPatientCommand(admission);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
