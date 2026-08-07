using Assets.Scripts.Enemy.Services;
using Assets.Scripts.Health;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Enemy
{
    public class EnemyLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EnemyModel>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<MovementService>().AsSelf();

            builder.Register<HealthService>(Lifetime.Singleton).AsSelf();
        }
    }
}
