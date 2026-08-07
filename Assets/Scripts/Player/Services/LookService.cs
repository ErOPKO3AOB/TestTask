using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player.Services
{
    public class LookService : IFixedTickable
    {
        public LookService(PlayerView playerView)
        {
            _playerView = playerView;
        }

        private readonly PlayerView _playerView;

        public void FixedTick()
        {
            Vector3 velocity = _playerView.rb.linearVelocity;
            velocity.y = 0f;

            if (velocity.sqrMagnitude < 0.01f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);

            _playerView.rb.rotation = Quaternion.Slerp(
                _playerView.rb.rotation,
                targetRotation,
                _playerView.configs.rotationSpeed * Time.deltaTime
            );
        }
    }
}