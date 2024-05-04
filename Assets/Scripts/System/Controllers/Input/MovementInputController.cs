using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common.LocalInput;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UnityEngine;

namespace System
{
    [UsedImplicitly]
    internal sealed class MovementInputController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicVariable<Vector3> _moveDirection;

        private readonly InputFacade _input;
        private readonly IAtomicObject _movable;

        internal MovementInputController(InputFacade input, IAtomicObject movable)
        {
            _input = input;
            _movable = movable;
        }

        void IStartGameListener.OnStart()
        {
            _moveDirection = _movable.GetVariable<Vector3>(MovableAPI.MovementDirection);
        }

        void IUpdateGameListener.OnUpdate()
        {
            float horizontal = _input.HorizontalAxis;
            float vertical = _input.VerticalAxis;
            
            _moveDirection.Value = new Vector3(horizontal, 0, vertical).normalized;
        }
    }
}