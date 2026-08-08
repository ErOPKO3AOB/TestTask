using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player.Services
{
    public class AutoAimService : IInitializable, ITickable
    {
        public AutoAimService(PlayerView playerView, GameConfigs gameConfigs)
        {
            _playerView = playerView;
            _gameConfigs = gameConfigs;
        }

        private readonly PlayerView _playerView;
        private float _delayTime = 0.2f;
        private float _nextTime;

        private readonly RaycastHit[] _raycastHits = new RaycastHit[1];
        private readonly GameConfigs _gameConfigs;

        public Transform Target { get; private set; }
        private GameObject _aimInstance;

        public void Initialize()
        {
            _nextTime = Time.time;
        }

        public void Tick()
        {
            if (Time.time < _nextTime) return;

            _nextTime = Time.time + _delayTime;

            var configs = _playerView.configs;
            int hits = Physics.SphereCastNonAlloc(
                _playerView.transform.position,
                configs.autoAimDistance,
                _playerView.transform.forward,
                _raycastHits,
                configs.autoAimRange,
                configs.autoAimLM,
                QueryTriggerInteraction.Ignore
            );

            if (hits > 0 && _raycastHits[0].distance <= configs.autoAimRange)
            {
                Target = _raycastHits[0].transform;
                if (_aimInstance == null)
                    _aimInstance = GameObject.Instantiate(_gameConfigs.aim);

                _aimInstance.SetActive(true);
                _aimInstance.transform.SetParent(Target.transform);
                _aimInstance.transform.localPosition = new Vector3(0, -0.45f, 0);
                _aimInstance.transform.localRotation = Quaternion.identity;
            }
            else
            {
                if (_aimInstance != null)
                    _aimInstance.SetActive(false);

                Target = null;
            }
        }

    }
}
