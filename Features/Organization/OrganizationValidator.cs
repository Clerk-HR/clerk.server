using System.Data;
using FluentValidation;

namespace clerk.server.Features.Organization;

public class OrganizationValidator : AbstractValidator<CreateOrganizationDto>
{


    private readonly string pattern = @"^[a-zA-Z][a-zA-Z_-]*[a-zA-Z]$";
    public OrganizationValidator()
    {

        RuleFor(o => o.Name)
        .NotEmpty().WithMessage("name is a required field")
        .MaximumLength(64).WithMessage("name cannot exceed 64 characters");

    }
}