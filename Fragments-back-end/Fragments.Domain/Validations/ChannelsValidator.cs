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
    public class ChannelsValidator : AbstractValidator<ChannelsOfRefferenceDTO>
    {
        public ChannelsValidator()
        {
            RuleFor(x => x.ChannelName).NotEmpty().WithMessage("Empty channel name");

            RuleFor(x => x.ChannelDetails).NotEmpty().WithMessage("Empty channel details");
        }
    }
}
