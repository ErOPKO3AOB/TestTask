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
            builder.Register<PlayerModel>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<MoveService>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<LookService>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<AutoAimService>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<HealthService>(Lifetime.Singleton).AsSelf();
        }
    }
}
