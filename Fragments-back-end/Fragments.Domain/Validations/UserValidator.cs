using FluentValidation;
using Fragments.Domain.Dto;

namespace Fragments.Domain.ValidationAttributes
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.Birthday).LessThan(DateTime.Now).WithMessage("Invalid date");
        }
    }
}
