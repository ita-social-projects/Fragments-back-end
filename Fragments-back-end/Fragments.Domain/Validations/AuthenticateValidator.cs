using FluentValidation;
using Fragments.Domain.Dto;
using Fragments.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Validations
{
    public class AuthenticateValidator : AbstractValidator<AuthenticateRequestDTO>
    {
        public AuthenticateValidator(IUserService service)
        {
            RuleFor(x => x.Email).Must(email => service.IsEmailAlreadyExistsAsync(email).Result).WithMessage("User not found!");
        }
    }
}
