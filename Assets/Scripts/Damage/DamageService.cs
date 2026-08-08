using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Damage
{
    public class DamageService : IInitializable
    {
        public DamageService([Key("DamagePoint")]Transform damagePoint, DamageConfigs damageConfigs)
        {
            _damagePoint = damagePoint;
            _damageConfigs = damageConfigs;
        }

        private readonly Transform _damagePoint;
        private readonly DamageConfigs _damageConfigs;

        private readonly RaycastHit[] _raycastHits = new RaycastHit[1];

        private float _nextTime;

        public void Initialize()
        {
            _nextTime = Time.time;
        }

        public void TakeDamage()
        {
            if (Time.time < _nextTime) return;
            _nextTime = Time.time + _damageConfigs.delay;

            if (Physics.RaycastNonAlloc(_damagePoint.position, _damagePoint.forward, _raycastHits, 0.5f, _damageConfigs.layerMask) > 0)
            {
                if (_raycastHits[0].collider.gameObject.TryGetComponent<IDamagable>(out var damagable))
                {
                    damagable.TakeDamage(_damageConfigs.damage);
                }
            }
        }
    }
}
