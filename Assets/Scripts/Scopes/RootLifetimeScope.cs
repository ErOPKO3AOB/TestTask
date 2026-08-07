using Assets.Scripts.Inputs;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Scopes
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MobileInputReader>(Lifetime.Singleton)
                .As<IInputReader>();
        }
    }
}
