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
            var playerView = _resolver.Instantiate(_gameConfigs.playerPrefab, position, rotation).GetComponent<PlayerView>();

            // Spawning player scope
            _gameLifetimeScope.CreateChild<PlayerLifetimeScope>(builder => 
            {
                builder.RegisterInstance(playerView);
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
