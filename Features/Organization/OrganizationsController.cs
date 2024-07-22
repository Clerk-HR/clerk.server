using System.Security.Claims;
using clerk.server.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace clerk.server.Features.Organization;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private IOrganizationService _service;

    public OrganizationsController(IOrganizationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return BadRequest(Result.Unauthorized());

        var result = await _service.CreateOrganization(Guid.Parse(userId), dto);

        if (!result.IsSuccess) return BadRequest(result.ErrorResult);

        return Ok(result.SuccessResult);
    }

    [HttpPost("join")]
    public async Task<IActionResult> JoinOrganization([FromBody] JoinDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return BadRequest(Result.Unauthorized());

        var result = await _service.JoinOrganization(Guid.Parse(userId), dto);
        if (!result.IsSuccess) return BadRequest(result.ErrorResult);

        return Ok(result.SuccessResult);
    }

    [HttpGet("members")]
    public async Task<IActionResult> GetMembers(Guid organizationId)
    {
        var result = await _service.GetMembers(organizationId);
        if (!result.IsSuccess) return BadRequest(result.ErrorResult);

        return Ok(result.SuccessResult);    

    }


}