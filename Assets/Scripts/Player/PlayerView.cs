using Assets.Scripts.Health;
using System;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [Inject]
        public void Construct(PlayerModel playerModel)
        {
            PlayerModel = playerModel;
        }

        public PlayerModel PlayerModel { get; private set; }

        public PlayerConfigs configs;
        public Rigidbody rb;
    }
}
