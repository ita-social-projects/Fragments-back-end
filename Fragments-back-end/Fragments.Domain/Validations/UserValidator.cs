﻿using FluentValidation;
using Fragments.Domain.Dto;
using Fragments.Domain.Services;

namespace Fragments.Domain.Validations
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).EmailAddress();

            RuleFor(user => user.Birthday).LessThan(DateTime.Now).WithMessage("Invalid date");

            RuleForEach(user => user.ChannelsOfRefferences).SetValidator(new ChannelsValidator());

        }
    }
}
