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
    internal sealed class RotateInputController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicVariable<Vector3> _rotationDirection;
        private IAtomicValue<Vector3> _movementDirection;

        private readonly InputFacade _input;
        private readonly Camera _camera;
        private readonly Transform _transform;
        private readonly IAtomicObject _rotatable;

        internal RotateInputController(InputFacade input, Camera camera, Transform transform, IAtomicObject rotatable)
        {
            _input = input;
            _camera = camera;
            _transform = transform;
            _rotatable = rotatable;
        }

        void IStartGameListener.OnStart()
        {
            _rotationDirection = _rotatable.GetVariable<Vector3>(RotatableAPI.RotationDirection);
            _movementDirection = _rotatable.GetVariable<Vector3>(MovableAPI.MovementDirection);
        }
        
        void IUpdateGameListener.OnUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = _camera.ScreenPointToRay(_input.MousePosition);

                if (!Physics.Raycast(ray, out RaycastHit hit)) return;
            
                Vector3 targetPosition = hit.point;
            
                Vector3 direction = (targetPosition - _transform.position).normalized;
                direction.y = 0;

                _rotationDirection.Value = direction;
                
                return;
            }
            
            _rotationDirection.Value = _movementDirection.Value;
        }
    }
}