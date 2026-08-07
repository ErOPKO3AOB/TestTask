using Assets.Scripts;
using Assets.Scripts.Enemy;
using Assets.Scripts.Enemy.Services;
using Assets.Scripts.ObjectPool;
using Assets.Scripts.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameConfigs _gameConfigs;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameConfigs);

        builder.Register<PlayerFactory>(Lifetime.Singleton).AsSelf();

        builder.Register<EnemyFactory>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<EnemySpawnerService>(Lifetime.Singleton).AsSelf();

        builder.RegisterEntryPoint<ObjectPoolService>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<GameInitializer>();
    }
}
