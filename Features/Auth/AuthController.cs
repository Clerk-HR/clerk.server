
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clerk.server.Features.Auth;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] RegisterDto dto)
    {
        var result = await _service.CreateAccount(dto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorResult);
        }
        return Ok(result.SuccessResult);
    }


}