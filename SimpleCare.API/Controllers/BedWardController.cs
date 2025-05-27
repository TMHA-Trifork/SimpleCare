using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCare.BedWards.Application.Commands;
using SimpleCare.BedWards.Application.Queries;
using SimpleCare.BedWards.Application.Values;
using SimpleCare.BedWards.Domain;
using SimpleCare.Infrastructure.Interfaces.Authorization;

using System.Net.Mime;

namespace SimpleCare.API.Controllers
{
    [Route("api/bed-ward")]
    [ApiController]
    [Authorize]
    public class BedWardController(IMediator mediator, IAuthorizationService authorization) : ControllerBase
    {
        [HttpGet("patients", Name = "GetBedWardPatients")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<BedWardPatientListItem[]>> GetPatients(Guid wardId, CancellationToken cancellationToken)
        {
            var authorized = await authorization.AuthorizeAsync(
                User,
                new SimpleCareResource(typeof(Ward), wardId),
                new SimpleCareAuthorizationRequirement(AccessLevel.Read));
            if (!authorized.Succeeded)
                return Forbid();

            var query = new GetPatientsQuery(wardId);
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("patients/{patientId}", Name = "GetBedWardPatient")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<BedWardPatient>> GetPatient(Guid patientId, CancellationToken cancellationToken)
        {
            var query = new GetPatientQuery(patientId);
            var patient = await mediator.Send(query, cancellationToken);

            var authorized = await authorization.AuthorizeAsync(
                User,
                new SimpleCareResource(typeof(Ward), patient.WardId),
                new SimpleCareAuthorizationRequirement(AccessLevel.Read));
            if (!authorized.Succeeded)
                return Forbid();

            return Ok(patient);
        }

        [HttpPost("patients/admit", Name = "AdmitPatient")]
        public async Task<ActionResult> Admit(PatientAdmission admission, CancellationToken cancellationToken)
        {
            var authorized = await authorization.AuthorizeAsync(
                User,
                new SimpleCareResource(typeof(Ward), admission.WardId),
                new SimpleCareAuthorizationRequirement(AccessLevel.Write));
            if (!authorized.Succeeded)
                return Forbid();

            var command = new AdmitPatientCommand(admission);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
