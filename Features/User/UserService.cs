using clerk.server.Data.Models;
using clerk.server.Data.Repository;
using clerk.server.Features.Member;
using clerk.server.Features.Organization;
using clerk.server.Helpers;
using Microsoft.EntityFrameworkCore;

namespace clerk.server.Features.User;

public interface IUserService
{
    Task<Result> GetCurrentUser(Guid userId);
    Task<Result> PostUserDetails(Guid userId, UserDetailsDto dto);
}

public class UserService : IUserService
{
    private readonly RepositoryContext _repository;
    public UserService(RepositoryContext repository)
    {
        _repository = repository;
    }

    public async Task<Result> GetCurrentUser(Guid userId)
    {
        var user = await _repository.Users.Where(u => u.Id == userId).Select(x => new UserDto()
        {
            Id = x.Id,
            Fullname = x.Fullname,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            AvatarUrl = x.AvatarUrl,
            OnBoarding = x.OnBoarding,
            CreatedOn = x.CreatedOn,
            Profile = x.Profile == null ? null : new MemberDto
            {
                Id = x.Profile.Id,
                Roles = x.Profile.Roles,
                Organization = new OrganizationDto
                {
                    Id = x.Profile.Organization.Id,
                    Description = x.Profile.Organization.Description,
                    Name = x.Profile.Organization.Name,
                    logoUrl = x.Profile.Organization.LogoUrl,
                    InviteCode = x.Profile.Roles.Contains(Role.Manager) ? x.Profile.Organization.InviteCode : null
                }
            }

        }).FirstOrDefaultAsync();

        if (user == null) return Result.Unauthorized();

        return Result.Success("signed-in", user);
    }

    public async Task<Result> PostUserDetails(Guid userId, UserDetailsDto dto)
    {
        var user = await _repository.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
        if (user == null) return Result.Failure(["user not found"]);

        var validationResult = new UserValidator().Validate(dto);

        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

        }

        if (dto.Avatar != null)
        {
            user.AvatarUrl = dto.Avatar.Name;
        }

        user.Fullname = dto.FullName;
        user.PhoneNumber = dto.PhoneNumber;
        user.OnBoarding = OnBoarding.JoinCreate;

        _repository.Update(user);
        await _repository.SaveChangesAsync();

        return Result.Success("profile updated successfully");

    }
}