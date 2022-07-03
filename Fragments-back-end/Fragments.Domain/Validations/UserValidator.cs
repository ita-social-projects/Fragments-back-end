using FluentValidation;
using Fragments.Domain.Dto;
using Fragments.Domain.Services;

namespace Fragments.Domain.ValidationAttributes
{
    public class UserValidator:AbstractValidator<UserDTO>
    {
        public UserValidator(IUserService userService)
        {
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.Birthday).LessThan(DateTime.Now).WithMessage("Invalid date");
        }
    }
}
