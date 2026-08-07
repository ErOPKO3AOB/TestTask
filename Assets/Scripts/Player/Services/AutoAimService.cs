using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player.Services
{
    public class AutoAimService : IFixedTickable
    {
        public AutoAimService(PlayerView playerView)
        {
            _playerView = playerView;
        }

        private readonly PlayerView _playerView;

        public void FixedTick()
        {
            //if (Physics.SphereCastNonAlloc())
            //{

            //}
        }
    }
}
