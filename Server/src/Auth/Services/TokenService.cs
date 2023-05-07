using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Interfaces;
using Core.Options;
using Core.UserAggregate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Services;

/// <inheritdoc />
internal sealed class TokenService : ITokenService
{
  private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
  private readonly AuthTokenOptions _options;

  public TokenService(IOptions<AuthTokenOptions> options, JwtSecurityTokenHandler jwtSecurityTokenHandler)
  {
    _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
    _options = options.Value;
  }

  /// <inheritdoc />
  public string GetAuthToken(User user)
  {
    var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, user.Id.ToString()) };

    claims.AddRange(user.UserRoles.Select(role => new Claim(ClaimTypes.Role, role.Role.Name!)));

    var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey));
    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
    var tokeOptions = new JwtSecurityToken(
      issuer: _options.Issuer,
      audience: _options.Audience,
      claims,
      expires: DateTime.Now.AddHours(_options.LifeTimeHours),
      signingCredentials: signingCredentials
    );

    return _jwtSecurityTokenHandler.WriteToken(tokeOptions);
  }
}
