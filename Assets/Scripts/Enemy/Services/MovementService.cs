using Assets.Scripts.Player;
using VContainer.Unity;

namespace Assets.Scripts.Enemy.Services
{
    public class MovementService : IStartable, IFixedTickable
    {
        public MovementService(EnemyView enemyView, PlayerView playerView)
        {
            _enemyView = enemyView;
            _playerView = playerView;
        }

        private readonly EnemyView _enemyView;
        private readonly PlayerView _playerView;

        public void Start()
        {
            _enemyView.navMeshAgent.speed = _enemyView.configs.moveSpeed;
            _enemyView.navMeshAgent.angularSpeed = _enemyView.configs.rotationSpeed;
            _enemyView.navMeshAgent.acceleration = _enemyView.configs.rotationSpeed * 2;
        }

        public void FixedTick()
        {
            _enemyView.navMeshAgent.SetDestination(_playerView.transform.position);
        }
    }
}
