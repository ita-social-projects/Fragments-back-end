using Fragments.Domain.Dto;
using Fragments.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragments.Domain.Validations.Attributes
{
    public class AuthenticateValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {

            var request = value as AuthenticateRequestDTO;
            if (request == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
