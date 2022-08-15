using FluentValidation;
using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;

namespace Fragments.Domain.Validations
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator(IUserService service)
        {
            RuleFor(user => user.Email).EmailAddress();

            RuleFor(user => user.Birthday).LessThan(DateTime.Now).WithMessage("Invalid date");

            RuleForEach(user => user.ChannelsOfRefferences).SetValidator(new ChannelsValidator());
        }
    }
}
