using UnityEngine;
using UnityEngine.Events;
using VContainer.Unity;

namespace Assets.Scripts.Health
{
    public class HealthService : IInitializable
    {
        public HealthService(HealthConfigs healthConfigs)
        {
            _healthConfigs = healthConfigs;
        }

        private readonly HealthConfigs _healthConfigs;

        private float _currentHealth;

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _healthConfigs.maxHealth;

        public event UnityAction<float> HealthChanged;

        public void Initialize()
        {
            _currentHealth = _healthConfigs.maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            Debug.Log("DAMAGE:" + damage + " heatlh" +  _currentHealth);
            if (_currentHealth < 0)
                _currentHealth = 0;

            HealthChanged?.Invoke(_currentHealth);
        }
    }
}
