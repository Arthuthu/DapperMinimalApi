using DataAccess.Models;
using FluentValidation;

namespace DataAccess.Validators;

public class UserValidator : AbstractValidator<UserModel>
{
	public UserValidator()
	{
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2);
    }
}
