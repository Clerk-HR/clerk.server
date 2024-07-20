
using System.Security.Claims;
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _service.Register(dto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorResult);
        }
        return Ok(result.SuccessResult);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _service.Login(dto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorResult);
        }
        return Ok(result.SuccessResult);
    }



}