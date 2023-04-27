using Auth.Endpoints.Register;

namespace Auth.Extensions;

internal static class TodoEntityExtensions
{
    internal static RegisterCommand ToCommand(this RegisterRequest command)
    {
        return new RegisterCommand
        {
            Email = command.Email,
            Password = command.Password,
            FirstName = command.FirstName,
            LastName = command.LastName,
        };
    }
}
