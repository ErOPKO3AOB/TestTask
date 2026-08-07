using Assets.Scripts.Health;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerConfigs", menuName = "Game/PlayerConfig")]
    public class PlayerConfigs : ScriptableObject
    {
        [Header("Health")]
        public HealthConfigs healthConfigs;

        [Header("Movement")]
        public float moveSpeed = 5;
        public float rotationSpeed = 12;
    }
}
