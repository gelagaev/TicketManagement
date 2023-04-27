using Auth.Endpoints.Register;

namespace Auth.Extensions;

internal static class RegisterRequestExtensions
{
    internal static RegisterCommand ToCommand(this RegisterRequest request)
    {
        return new RegisterCommand
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
    }
}
