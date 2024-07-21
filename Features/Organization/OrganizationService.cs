using clerk.server.Data.Models;
using clerk.server.Data.Repository;
using clerk.server.Features.Member;
using clerk.server.Features.User;
using clerk.server.Helpers;
using Microsoft.EntityFrameworkCore;

namespace clerk.server.Features.Organization;

public interface IOrganizationService
{
    Task<Result> CreateOrganization(Guid userId, CreateOrganizationDto dto);
    Task<Result> JoinOrganization(Guid userId, JoinDto dto);
}

public class OrganizationService : IOrganizationService
{
    private RepositoryContext _repository;
    public OrganizationService(RepositoryContext repository)
    {
        _repository = repository;
    }
    public async Task<Result> CreateOrganization(Guid userId, CreateOrganizationDto dto)
    {
        var user = await _repository.Users.FindAsync(userId);

        if (user == null)
        {
            return Result.Failure(["user not found"]);
        }


        var validationResult = new OrganizationValidator().Validate(dto);

        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        user.OnBoarding = OnBoarding.Complete;
        var organization = new OrganizationModel()
        {
            Name = dto.Name,
            LogoUrl = dto.logoUrl,
            InviteCode = GenerateCode(),
            Description = dto.Description
        };

        var member = new MemberModel()
        {
            User = user,
            Roles = [Role.Manager],
        };

        organization.Members = [member];

        await _repository.Organizations.AddAsync(organization);

        await _repository.SaveChangesAsync();

        var organizationDto = new OrganizationDto()
        {
            Id = organization.Id,
            Name = organization.Name,
            Description = organization.Description,
            InviteCode = organization.InviteCode,
            logoUrl = organization.LogoUrl,
            Members = organization.Members.Select(x => new MemberDto
            {
                Id = x.Id,
                User = new UserDto
                {
                    Id = x.User.Id,
                    Fullname = x.User.Fullname,
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    AvatarUrl = x.User.AvatarUrl,
                },
                Roles = x.Roles,
                Organization = null
            }).ToList()
        };

        return Result.Success(organizationDto);

    }

    public async Task<Result> JoinOrganization(Guid userId, JoinDto dto)
    {
        var user = await _repository.Users.FindAsync(userId);

        if (user == null)
        {
            return Result.Failure(["user not found"]);
        }

        var organization = await _repository.Organizations.SingleOrDefaultAsync(o => o.InviteCode == dto.Code);

        if (organization == null)
        {
            return Result.Failure(["invalid access code"]);
        }

        var newMember = new MemberModel()
        {
            User = user,
            Roles = [Role.Employee],
            Organization = organization,
        };

        organization.Members = [newMember];

        await _repository.Members.AddAsync(newMember);
        await _repository.SaveChangesAsync();

        return Result.Success("Welcome to " + organization.Name);

    }
    private static string GenerateCode()
    {
        var _random = new Random();

        char letter = (char)_random.Next('A', 'Z' + 1);

        string digits = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            digits += _random.Next(0, 10).ToString();
        }

        return letter + digits;
    }

}
