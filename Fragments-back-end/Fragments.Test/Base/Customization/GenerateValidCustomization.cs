using AutoFixture;
using Fragments.Data.Entities;
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
            x.Email,
            fixture.Create<MailAddress>().ToString())
            .With(x =>
            x.Birthday,
            fixture.Create<DateTime>())
            .Without(x =>
            x.ChannelsOfRefferences
            ));

            fixture.Customize<User>(composer =>
            composer
            .With(x =>
            x.Email,
            fixture.Create<MailAddress>().ToString())
            .With(x =>
            x.Birthday,
            fixture.Create<DateTime>())
            .Without(x =>
            x.ChannelsOfRefferences
            )
            .Without(x => 
            x.Notifications));
        }
    }
}
