using FluentValidation;
using Fragments.Domain.Dto;
using Fragments.Domain.Services;

namespace Fragments.Domain.Validations
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator(IUserService service)
        {
            RuleFor(user => user.Email).Must(email => !service.IsEmailAlreadyExistsAsync(email).Result);

            RuleFor(user => user.Email).EmailAddress();

            RuleFor(user => user.Birthday).LessThan(DateTime.Now).WithMessage("Invalid date");

            RuleForEach(user => user.ChannelsOfRefferences).SetValidator(new ChannelsValidator());
        }
    }
}
