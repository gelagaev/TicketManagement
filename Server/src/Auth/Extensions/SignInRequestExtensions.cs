using Auth.Endpoints.SignIn.V1;

namespace Auth.Extensions;

internal static class SigninRequestExtensions
{
  internal static SignInCommand ToCommand(this SignInRequest request)
  {
    return new SignInCommand
    {
      Email = request.Email,
      Password = request.Password,
    };
  }
}
