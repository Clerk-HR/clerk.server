using System.Data;
using FluentValidation;

namespace clerk.server.Features.User;

public class UserValidator : AbstractValidator<UserDetailsDto>
{

    const string fullName = @"^[a-zA-z]{4,}(?: [a-zA-Z]+){0,2}$";
    const string phoneNumber = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
    public UserValidator()
    {
        RuleFor(u => u.FullName)
        .NotEmpty().WithMessage("Fullname must not be empty")
        .MinimumLength(4).WithMessage("Fullname must be at least 4 characters")
        .Matches(fullName).WithMessage("Fullname must contain letters only with an optional space");

        RuleFor(u => u.PhoneNumber)
        .NotEmpty().WithMessage("Fullname must not be empty")
        .Length(10).WithMessage("Contact must be at least 10 digits")
        .Matches(phoneNumber).WithMessage("Contact is invalid");


    }

}