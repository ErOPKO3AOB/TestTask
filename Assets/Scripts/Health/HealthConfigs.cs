using UnityEngine;

namespace Assets.Scripts.Health
{
    [CreateAssetMenu(fileName = "HealthConfigs", menuName = "Game/HealthConfigs")]
    public class HealthConfigs : ScriptableObject
    {
        public float maxHealth;
        public bool autoHeal;
        public float healDelay;
        public float healSpeed;
    }
}
