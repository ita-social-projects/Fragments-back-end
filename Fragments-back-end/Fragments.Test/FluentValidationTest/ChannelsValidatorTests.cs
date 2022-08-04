using FluentAssertions.Execution;
using FluentValidation.TestHelper;
using Fragments.Domain.Dto;
using Fragments.Domain.Validations;

namespace Fragments.Test.FluentValidationTest
{
    public class ChannelsValidatorTests
    {
        private readonly ChannelsValidator validator;

        public ChannelsValidatorTests()
        {
            validator = new ChannelsValidator();
        }

        [Theory]
        [InlineData("name")]
        public void ChannelName_IsNotEmpty_NotGeneratesValidationError(string name)
        {
            //Arrange
            var actual = new ChannelsOfRefferenceDto { ChannelName = name };

            //Act
            var result = validator.TestValidate(actual);

            //Assert
            result.ShouldNotHaveValidationErrorFor(channel => channel.ChannelName);
        }
        [Theory]
        [InlineData("", "")]
        public void ChannelName_IsEmpty_GeneratesValidationError(string name, string details)
        {
            //Arrange
            var actual = new ChannelsOfRefferenceDto { ChannelName = name, ChannelDetails = details };

            //Act
            var result = validator.TestValidate(actual);

            //Assert
            using (new AssertionScope())
            {
                result.ShouldHaveValidationErrorFor(channel => channel.ChannelName);
                result.ShouldHaveValidationErrorFor(channel => channel.ChannelDetails);
            }
        }
        [Theory]
        [InlineData("details")]
        public void ChannelDetails_IsNotEmpty_NotGeneratesValidationError(string details)
        {
            //Arrange
            var actual = new ChannelsOfRefferenceDto { ChannelDetails = details, };

            //Act
            var result = validator.TestValidate(actual);

            //Assert
            result.ShouldNotHaveValidationErrorFor(channel => channel.ChannelDetails);
        }

    }
}
