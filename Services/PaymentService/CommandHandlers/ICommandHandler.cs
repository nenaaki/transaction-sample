using PaymentService.Commands;

namespace PaymentService.CommandHandlers;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}