using Assets.Scripts.Player;
using VContainer.Unity;

namespace Assets.Scripts.Enemy.Services
{
    public class MovementService : IFixedTickable
    {
        public MovementService(EnemyView enemyView, PlayerView playerView)
        {
            _enemyView = enemyView;
            _playerView = playerView;
        }

        private readonly EnemyView _enemyView;
        private readonly PlayerView _playerView;

        public void FixedTick()
        {
            _enemyView.navMeshAgent.SetDestination(_playerView.transform.position);
        }
    }
}
