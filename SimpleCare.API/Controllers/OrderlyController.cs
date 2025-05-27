using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleCare.Orderlies.Application.Commands;
using SimpleCare.Orderlies.Application.Queries;
using SimpleCare.Orderlies.Application.Values;

using System.Net.Mime;

namespace SimpleCare.API.Controllers
{
    [Route("api/orderly")]
    [ApiController]
    [Authorize]
    public class OrderlyController(IMediator mediator, ILogger<OrderlyController> logger) : ControllerBase
    {
        [HttpGet("tasks", Name = "GetOrderlyTasks")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<OrderlyTaskListItem[]>> GetOrderlyTasks(CancellationToken cancellationToken)
        {
            var query = new GetOrderlyTasksQuery();
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost("Tasks/start", Name = "StartTask")]
        public async Task<ActionResult> StartTask(Guid taskId, CancellationToken cancellationToken)
        {
            var command = new StartOrderlyTaskCommand(taskId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpPost("Tasks/end", Name = "EndTask")]
        public async Task<ActionResult> EndTask(Guid taskId, CancellationToken cancellationToken)
        {
            var command = new EndOrderlyTaskCommand(taskId);
            await mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
