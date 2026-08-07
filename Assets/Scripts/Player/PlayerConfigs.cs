using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "PlayerConfigs", menuName = "Game/PlayerConfig")]
    public class PlayerConfigs : ScriptableObject
    {
        public float moveSpeed;
    }
}
