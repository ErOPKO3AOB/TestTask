using Assets.Scripts.Enemy;
using Assets.Scripts.Enemy.Services;
using Assets.Scripts.Player;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts
{
    public class GameInitializer : IStartable
    {
        public GameInitializer(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        private readonly PlayerFactory _playerFactory;

        public void Start()
        {
            _playerFactory.Create(Vector3.zero, Quaternion.identity);
        }
    }
}
