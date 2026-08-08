using Assets.Scripts.Damage;
using Assets.Scripts.Health;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfigs", menuName = "Game/EnemyConfigs")]
    public class EnemyConfigs : ScriptableObject
    {
        [Header("Health/Damage")]
        public HealthConfigs healthConfigs;
        public DamageConfigs damageConfigs;

        [Header("Movement")]
        public float moveSpeed;
        public float rotationSpeed;
    }
}
