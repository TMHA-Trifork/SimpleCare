using MediatR;

using SimpleCare.Infrastructure.Interfaces.UnitOfWork;
using SimpleCare.Orderlies.Domain.Interfaces;

namespace SimpleCare.Orderlies.Application.Commands;

public record StartOrderlyTaskCommand(Guid TaskId) : IRequest;

public class StartOrderlyTaskCommandHandler(IUnitOfWork unitOfWork, IOrderly orderlyRoot) : IRequestHandler<StartOrderlyTaskCommand>
{
    public async Task Handle(StartOrderlyTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await orderlyRoot.StartTask(request.TaskId, cancellationToken);

            await unitOfWork.SaveChanges(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while starting the task", ex);
        }
    }
}