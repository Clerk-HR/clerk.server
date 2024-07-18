
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clerk.server.Features.User;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    public UsersController(UserService service)
    {
        _service = service;
    }

    [HttpGet("current-user/{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var result = await _service.GetUser(id);
        if (!result.IsSuccess) return BadRequest(result.ErrorResult);

        return Ok(result.SuccessResult);
    }

}