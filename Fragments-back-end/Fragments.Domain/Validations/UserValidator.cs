using FluentValidation;
using Fragments.Data.Context;
using Fragments.Domain.Models;
using Fragments.Domain.Services;
using System.ComponentModel.DataAnnotations;

namespace Fragments.Domain.ValidationAttributes
{
    public class UserValidator:AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).EmailAddress();
            RuleFor(user => user.Birthday).LessThan(DateTime.Now).WithMessage("Invalid date");
        }
    }
}
