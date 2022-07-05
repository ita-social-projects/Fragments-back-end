using System.Collections.Generic;
using AutoFixture;
using AutoMapper;
using Fragments.Domain.Profiles;

namespace Fragments.Test.Base
{
    public class Base
    {
        protected Fixture Fixture { get; set; }

        protected IMapper Mapper { get; set; }

        public Base()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            Mapper = new Mapper(new MapperConfiguration(options =>
                options.AddProfiles(new List<Profile>
                    {
                        new UserProfile()
                    })));
        }
    }
}

