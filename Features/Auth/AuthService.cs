using clerk.server.Data.Models;
using clerk.server.Data.Repository;
using clerk.server.Features.Member;
using clerk.server.Features.Organization;
using clerk.server.Features.User;
using clerk.server.Helpers;
using Microsoft.EntityFrameworkCore;

namespace clerk.server.Features.Auth;

public interface IAuthService
{
    Task<Result> Register(RegisterDto dto);
    Task<Result> Login(LoginDto dto);
    Task<Result> GetCurrentUser(Guid userId);
}

public class AuthService : IAuthService
{
    private readonly RepositoryContext _repository;
    private readonly IJwtTokenManager _jwtTokenManager;
    public AuthService(RepositoryContext repository, IJwtTokenManager jwtTokenManager)
    {
        _repository = repository;
        _jwtTokenManager = jwtTokenManager;
    }

    public async Task<Result> Register(RegisterDto dto)
    {
        var isEmailTaken = await _repository.Users.AnyAsync(u => u.Email == dto.Email);

        if (isEmailTaken) return Result.Failure(["email is registered to an existing account"]);

        var validationResult = new AuthValidator().Validate(dto);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result.Failure(errors);
        }

        var newUser = new UserModel
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            OnBoarding = OnBoarding.UserDetails
        };

        await _repository.Users.AddAsync(newUser);
        await _repository.SaveChangesAsync();

        var response = new AuthResponseDto
        {
            AccessToken = _jwtTokenManager.GenerateAccessToken(newUser.Id)
        };

        return Result.Success("account created successfully", response);

    }

    public async Task<Result> Login(LoginDto dto)
    {
        var user = await _repository.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (user == null) return Result.Failure(["bad credentials"]);

        var authenticationResult = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!authenticationResult)
        {
            return Result.Failure(["bad credentials"]);
        }

        var response = new AuthResponseDto
        {
            AccessToken = _jwtTokenManager.GenerateAccessToken(user.Id)
        };

        return Result.Success("signed-in successfully", response);
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
                }


            }

        }).FirstOrDefaultAsync();

        if (user == null) return Result.Failure(["user not found"]);

        return Result.Success("signed-in", user);
    }



}