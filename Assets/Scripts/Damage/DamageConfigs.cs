using UnityEngine;

namespace Assets.Scripts.Damage
{
    [CreateAssetMenu(fileName = "DamageConfigs", menuName = "Game/DamageConfigs")]
    public class DamageConfigs : ScriptableObject
    {
        public float damage;
        public float delay;
        public LayerMask layerMask;
    }
}
