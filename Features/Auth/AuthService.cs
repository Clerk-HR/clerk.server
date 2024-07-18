using BCrypt.Net;
using clerk.server.Data.Models;
using clerk.server.Data.Repository;
using clerk.server.Helpers;
using Microsoft.EntityFrameworkCore;

namespace clerk.server.Features.Auth;

public interface IAuthService
{
    Task<Result> CreateAccount(RegisterDto dto);
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

    public async Task<Result> CreateAccount(RegisterDto dto)
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
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _repository.Users.AddAsync(newUser);
        await _repository.SaveChangesAsync();

        var response = new AuthResponseDto
        {
            User = new()
            {
                Id = newUser.Id,
                Email = newUser.Email,
                OnBoarding = newUser.OnBoarding
            },
            Token = _jwtTokenManager.GenerateAccountAccessToken(newUser.Id)
        };

        return Result.Success("account created successfully", response);

    }
}