
using System.Security.Claims;
using clerk.server.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clerk.server.Features.User;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("current-user")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return BadRequest(Result.Unauthorized());

        var result = await _service.GetCurrentUser(Guid.Parse(userId));
        if (!result.IsSuccess) return BadRequest(result.ErrorResult);

        return Ok(result.SuccessResult);

    }

    [HttpPost("user-details")]
    public async Task<IActionResult> UpdateUserDetails([FromBody] UserDetailsDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return BadRequest(Result.Unauthorized());

        var result = await _service.PostUserDetails(Guid.Parse(userId), dto);

        if (!result.IsSuccess) return BadRequest(result.ErrorResult);

        return Ok(result.SuccessResult);
    }


}