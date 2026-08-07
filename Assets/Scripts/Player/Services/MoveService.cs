using Assets.Scripts.Inputs;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player.Services
{
    public class MoveService : IFixedTickable
    {
        public MoveService(PlayerView view, IInputReader inputReader)
        {
            _view = view;
            _inputReader = inputReader;
        }

        private readonly PlayerView _view;
        private readonly IInputReader _inputReader;

        public void FixedTick()
        {
            Vector2 input = _inputReader.MoveInput;

            Vector3 direction = new Vector3(input.x, 0f, input.y);

            if (direction.sqrMagnitude > 1f)
                direction.Normalize();

            Vector3 velocity = direction * _view.configs.moveSpeed;
            velocity.y = _view.rb.linearVelocity.y;

            _view.rb.linearVelocity = velocity;
        }
    }
}
