using System;
using Unity.Cinemachine;
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

        public PlayerView PlayerView { get; private set; }

        public PlayerView Create(Vector3 position, Quaternion rotation)
        {
            // Spawn player
            var playerView = GameObject.Instantiate(_gameConfigs.playerPrefab, position, rotation).GetComponent<PlayerView>();

            var canvas = GameObject.Instantiate(_gameConfigs.gameCanvas);

            // Spawning player scope
            _gameLifetimeScope.CreateChild<PlayerLifetimeScope>(builder => 
            {
                builder.RegisterComponent(playerView).AsSelf();
                builder.RegisterComponent(playerView.configs.damageConfigs).AsSelf();
                builder.RegisterComponent(playerView.configs.healthConfigs).AsSelf();

                builder.RegisterComponent(GameObject.Instantiate(_gameConfigs.healthBarUI, canvas.transform));
            });

            // Spawn camera
            var camera = _resolver.Instantiate(_gameConfigs.CM_FollowCamPrefab).GetComponent<CinemachineCamera>();
            camera.Target.TrackingTarget = playerView.transform;

            // Cache player
            PlayerView = playerView;

            return playerView;
        }
    }
}
