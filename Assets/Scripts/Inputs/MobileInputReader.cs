using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Assets.Scripts.Inputs
{
    public class MobileInputReader : IInputReader,
        IStartable,
        IDisposable
    {
        public MobileInputReader()
        {
            _inputActions = new();
        }

        private readonly PlayerInputActions _inputActions;
        public Vector2 MoveInput => _inputActions.Player.Move.ReadValue<Vector2>();
        
        public void Start()
        {
            _inputActions.Enable();
        }

        public void Dispose()
        {
            _inputActions.Disable();
            _inputActions.Dispose();
        }
    }
}
