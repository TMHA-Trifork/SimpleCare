using MediatR;

using SimpleCare.Infrastructure.Interfaces.UnitOfWork;
using SimpleCare.Orderlies.Domain.Interfaces;

namespace SimpleCare.Orderlies.Application.Commands;

public record EndOrderlyTaskCommand(Guid TaskId) : IRequest;

public class EndOrderlyTaskCommandHandler(IUnitOfWork unitOfWork, IOrderly orderlyRoot) : IRequestHandler<EndOrderlyTaskCommand>
{
    public async Task Handle(EndOrderlyTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await orderlyRoot.EndTask(request.TaskId, cancellationToken);

            await unitOfWork.SaveChanges(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while ending the task", ex);
        }
    }
}
