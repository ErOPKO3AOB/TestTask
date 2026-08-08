using Assets.Scripts.Damage;
using Assets.Scripts.Health;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerConfigs", menuName = "Game/PlayerConfig")]
    public class PlayerConfigs : ScriptableObject
    {
        [Header("Health/Damage")]
        public HealthConfigs healthConfigs;
        public DamageConfigs damageConfigs;

        [Header("Look")]
        public float autoAimDistance = 10f;
        public float autoAimRange = 8;
        public LayerMask autoAimLM;

        [Header("Movement")]
        public float moveSpeed = 5;
        public float rotationSpeed = 12;
    }
}
