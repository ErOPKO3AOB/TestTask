using Assets.Scripts.ObjectPool;
using Assets.Scripts.Player;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Enemy
{
    public class EnemyFactory
    {
        public EnemyFactory(IObjectResolver resolver, GameLifetimeScope gameLifetimeScope, GameConfigs gameConfigs, ObjectPoolService objectPoolService, PlayerFactory playerFactory)
        {
            _resolver = resolver;
            _gameLifetimeScope = gameLifetimeScope;
            _gameConfigs = gameConfigs;
            _playerFactory = playerFactory;

            _objectPoolService = objectPoolService;
        }

        private readonly IObjectResolver _resolver;
        private readonly GameLifetimeScope _gameLifetimeScope;
        private readonly GameConfigs _gameConfigs;
        private readonly PlayerFactory _playerFactory;

        private readonly ObjectPoolService _objectPoolService;

        public EnemyView Create(string name, Vector3 position = default, Quaternion rotation = default)
        {
            var enemyPrefab = _gameConfigs.enemies
                .Find(e => e.name == name)
                .prefab;

            if (enemyPrefab == null)
                throw new NullReferenceException($"Enemy with name {name} not found in {nameof(GameConfigs)}! Set enemies correctly!");

            //var enemyInstance = _objectPoolService.Spawn(enemyPrefab, position, rotation);
            var enemyView = GameObject.Instantiate(enemyPrefab, position, rotation).GetComponent<EnemyView>();
            //_resolver.Inject(enemyInstance);

            _gameLifetimeScope.CreateChild<EnemyLifetimeScope>(builder =>
            {
                builder.RegisterInstance(_playerFactory.PlayerView);
                builder.RegisterComponent(enemyView).AsSelf();

                builder.Register<Transform>(Lifetime.Singleton).AsSelf().Keyed("DamagePoint");
                builder.RegisterComponent(enemyView.configs.damageConfigs).AsSelf();
            });

            return null;
        }
    }
}
