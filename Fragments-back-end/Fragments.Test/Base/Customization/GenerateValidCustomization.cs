using AutoFixture;
using Fragments.Domain.Dto;
using Fragments.Test.Extentions;
using System.Net.Mail;

namespace Fragments.Test.Base.Customization
{
    public class GenerateValidCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<UserDTO>(composer =>
            composer
            .With(x =>
            x.Id,
            fixture.CreateInRange<Int32>(1,1))
            .With(x =>
            x.Email,
            fixture.Create<MailAddress>().ToString())
            .With(x =>
            x.Birthday,
            fixture.Create<DateTime>())
            .Without(x =>
            x.ChannelsOfRefferences
            ));

            //fixture.Customize<ChannelsOfRefferenceDTO>(composer =>
            //composer.With(x =>
            //x.UserId,
            //fixture.CreateInRange<Int32>(1, 1)));
        }
    }
}
