
using System.Net.Http.Headers;
using clerk.server.Data.Repository;
using clerk.server.Helpers;
using Microsoft.EntityFrameworkCore;

namespace clerk.server.Features.User;

public interface IUserService
{
    Task<Result> GetUser(Guid id);
}

public class UserService : IUserService
{
    private readonly RepositoryContext _repository;
    public UserService(RepositoryContext repository)
    {
        _repository = repository;
    }

    public async Task<Result> GetUser(Guid id)
    {
        var user = await _repository.Users.Where(u => u.Id == id).Select(x => new UserDto()
        {
            Id = x.Id,
            Email = x.Email,
            AvatarUrl = x.AvatarUrl,
            Fullname = x.Fullname,
            PhoneNumber = x.PhoneNumber,
            OnBoarding = x.OnBoarding

        }).FirstOrDefaultAsync();

        if (user == null) return Result.Failure(["user not found"]);

        return Result.Success("get user success", user);
    }
}