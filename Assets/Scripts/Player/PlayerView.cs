using Assets.Scripts.Damage;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerView : MonoBehaviour, IDamagable
    {
        public void Construct(PlayerModel playerModel)
        {
            PlayerModel = playerModel;
        }

        public PlayerModel PlayerModel { get; private set; }

        public PlayerConfigs configs;
        public Rigidbody rb;

        public void TakeDamage(float damage)
        {
            PlayerModel.healthService.TakeDamage(damage);
        }
    }
}
