using FluentValidation;
using Fragments.Domain.Dto;

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
