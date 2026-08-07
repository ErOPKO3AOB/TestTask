using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    public class PlayerFactory
    {
        public PlayerFactory(IObjectResolver resolver, GameLifetimeScope gameLifetimeScope, GameConfigs gameConfigs)
        {
            _resolver = resolver;
            _gameLifetimeScope = gameLifetimeScope;
            _gameConfigs = gameConfigs;
        }

        private readonly IObjectResolver _resolver;
        private readonly GameLifetimeScope _gameLifetimeScope;
        private readonly GameConfigs _gameConfigs;

        public PlayerView Create(Vector3 position, Quaternion quaternion)
        {
            var playerView = _resolver.Instantiate(_gameConfigs.playerPrefab).GetComponent<PlayerView>();

            _gameLifetimeScope.CreateChild<PlayerLifetimeScope>(builder => 
            {
                builder.RegisterInstance(playerView);
            });

            return playerView;
        }
    }
}
