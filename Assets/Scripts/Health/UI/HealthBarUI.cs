using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets.Scripts.Health.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [Inject]
        public void Construct(HealthService healthService)
        {
            _healthService = healthService;

            _slider.maxValue = _healthService.MaxHealth;
            _slider.value = _healthService.MaxHealth;

            _healthService.HealthChanged += HealthChanged;
        }

        private HealthService _healthService;

        [SerializeField] private Slider _slider;

        private void HealthChanged(float health)
        {
            _slider.value = health;
        }

        private void OnDestroy()
        {
            _healthService.HealthChanged -= HealthChanged;
            
        }
    }
}
