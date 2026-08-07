using Assets.Scripts.Health;
using Assets.Scripts.Player.Services;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    public class PlayerLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MoveService>().AsSelf();
            builder.RegisterEntryPoint<LookService>().AsSelf();

            builder.Register<HealthService>(Lifetime.Singleton).AsSelf();
        }
    }
}
