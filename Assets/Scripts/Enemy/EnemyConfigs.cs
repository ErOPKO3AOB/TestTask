using Assets.Scripts.Health;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfigs", menuName = "Game/EnemyConfigs")]
    public class EnemyConfigs : ScriptableObject
    {
        [Header("Health")]
        public HealthConfigs healthConfigs;

        [Header("Movement")]
        public float moveSpeed;
        public float rotationSpeed;
    }
}
