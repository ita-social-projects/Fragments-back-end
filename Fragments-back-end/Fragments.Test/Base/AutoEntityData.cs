using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Fragments.Test.Base.Customization;

namespace Fragments.Test.Base
{
    public class AutoEntityDataAttribute: AutoDataAttribute
    {
#pragma warning disable CS0618
        public AutoEntityDataAttribute()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            Fixture.Customize(new GenerateValidCustomization());
        }

    }
}
