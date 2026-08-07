using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfigs", menuName = "Game/EnemyConfigs")]
    public class EnemyConfigs : ScriptableObject
    {
        [Header("Health")]
        public float maxHealh;
        public float damage;

        [Header("Movement")]
        public float moveSpeed;
        public float rotationSpeed;
    }
}
