using Core;
using Core.UserAggregate;
using Core.UserAggregate.Specifications;
using Kernel.Interfaces;
using Microsoft.AspNetCore.Identity;
using WebApi.Interfaces;

namespace WebApi.Providers;

internal sealed class CurrentUserProvider : ICurrentUserProvider
{
  private readonly UserManager<User> _userManager;
  private readonly IHttpContextAccessor _contextAccessor;
  private readonly IRepository<User> _repository;

  public CurrentUserProvider(UserManager<User> userManager,
    IHttpContextAccessor contextAccessor,
    IRepository<User> repository)
  {
    _userManager = userManager;
    _contextAccessor = contextAccessor;
    _repository = repository;
  }

  public async Task<User> GetUserAsync()
  {
    var spec = new UserByIdWithRolesSpec(GetUserId());
    var user = await _repository.FirstOrDefaultAsync(spec);

    if (user == null)
    {
      throw new BadHttpRequestException(nameof(ErrorCodes.USER_NOT_FOUND_BY_CLAIMS_ERROR));
    }

    return user;
  }

  public Guid GetUserId()
  {
    var principal = _contextAccessor.HttpContext?.User;

    if (!Guid.TryParse(_userManager.GetUserId(principal), out var userId))
    {
      throw new BadHttpRequestException(nameof(ErrorCodes.PARSE_USER_ID_FROM_CLAIMS_ERROR));
    }

    return userId;
  }
}
