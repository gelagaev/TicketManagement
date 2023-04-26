using Auth.DTO;
using Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
  private readonly IUserService _userService;

  public AuthController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost]
  [Route("Login")]
  public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
  {
    var token = await _userService.GetAuthTokenOnUserLogin(request);

    if (!string.IsNullOrWhiteSpace(token))
    {
      return new LoginResponse { Token = token };
    }

    return NotFound();
  }

  [HttpPost]
  [Route("RegisterClient")]
  public async Task<RegisterResponse> RegisterClient([FromBody] RegisterRequest request)
  {
    return await _userService.RegisterUser(request);
  }
}
