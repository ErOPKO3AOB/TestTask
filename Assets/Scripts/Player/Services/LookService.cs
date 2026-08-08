using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player.Services
{
    public class LookService : IFixedTickable
    {
        public LookService(PlayerView playerView, AutoAimService autoAimService)
        {
            _playerView = playerView;
            _autoAimService = autoAimService;
        }

        private readonly PlayerView _playerView;
        private readonly AutoAimService _autoAimService;

        public void FixedTick()
        {
            Vector3 velocity = _playerView.rb.linearVelocity;
            velocity.y = 0f;

            if (velocity.sqrMagnitude < 0.01f)
                return;

            Quaternion targetRotation = _autoAimService.Target != null? 
                Quaternion.LookRotation(_autoAimService.Target.position - _playerView.transform.position)
                : Quaternion.LookRotation(velocity.normalized);

            _playerView.rb.rotation = Quaternion.Slerp(
                _playerView.rb.rotation,
                targetRotation,
                _playerView.configs.rotationSpeed * Time.deltaTime
            );
        }
    }
}