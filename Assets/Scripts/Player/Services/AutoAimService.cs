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
        private readonly GameConfigs _gameConfigs;

        private float _delayTime = 0.1f;
        private float _nextTime;
        
        //private readonly RaycastHit[] _raycastHits = new RaycastHit[16];
        private readonly Collider[] _overlapHits = new Collider[16];

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
            Transform nearest = FindNearestEnemy(configs);

            if (nearest != null)
            {
                Target = nearest;

                if (_aimInstance == null)
                    _aimInstance = Object.Instantiate(_gameConfigs.aim);

                _aimInstance.SetActive(true);
                _aimInstance.transform.SetParent(Target);
                _aimInstance.transform.localPosition = new Vector3(0f, -0.45f, 0f);
                _aimInstance.transform.localRotation = Quaternion.identity;
            }
            else
            {
                if (_aimInstance != null)
                    _aimInstance.SetActive(false);

                Target = null;
            }
        }

        private Transform FindNearestEnemy(PlayerConfigs configs)
        {
            // Вариант 1: чистый ближайший в радиусе (самый надёжный)
            int count = Physics.OverlapSphereNonAlloc(
                _playerView.transform.position,
                configs.autoAimRange,
                _overlapHits,
                configs.autoAimLM,
                QueryTriggerInteraction.Ignore
            );

            Transform nearest = null;
            float nearestDistSqr = float.MaxValue;
            Vector3 origin = _playerView.transform.position;

            for (int i = 0; i < count; i++)
            {
                Collider col = _overlapHits[i];
                if (col == null) continue;

                // Можно добавить дополнительную проверку, что это враг
                // if (!col.TryGetComponent<Enemy>(out _)) continue;

                float distSqr = (col.transform.position - origin).sqrMagnitude;
                if (distSqr < nearestDistSqr)
                {
                    nearestDistSqr = distSqr;
                    nearest = col.transform;
                }
            }

            return nearest;

            /* 
            // Вариант 2: если нужно учитывать направление вперёд (как было)
            // Раскомментируй этот блок и закомментируй OverlapSphere, если нужен "конус" вперёд

            int hits = Physics.SphereCastNonAlloc(
                _playerView.transform.position,
                configs.autoAimDistance,          // радиус сферы
                _playerView.transform.forward,
                _raycastHits,
                configs.autoAimRange,
                configs.autoAimLM,
                QueryTriggerInteraction.Ignore
            );

            Transform nearest = null;
            float nearestDist = float.MaxValue;

            for (int i = 0; i < hits; i++)
            {
                ref RaycastHit hit = ref _raycastHits[i];
                if (hit.collider == null) continue;

                if (hit.distance < nearestDist)
                {
                    nearestDist = hit.distance;
                    nearest = hit.transform;
                }
            }

            return nearest;
            */
        }
    }
}