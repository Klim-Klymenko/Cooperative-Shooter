using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class MoveComponent : IDisposable
    {
        [SerializeField] 
        private AtomicValue<float> _moveSpeed;
        
        private AtomicVariable<Vector3> _movementDirection = new();
        private readonly AndExpression _moveCondition = new();
        
        private MoveMechanics _moveMechanics;

        public IAtomicVariable<Vector3> MovementDirection => _movementDirection;
        public IAtomicExpression<bool> MoveCondition => _moveCondition;
        
        public void Compose(Transform transform, AtomicVariable<Vector3> direction = null)
        {
            if (direction != null)
                _movementDirection = direction;
            
            _moveCondition.Append(new AtomicFunction<bool>(() => _movementDirection.Value != Vector3.zero));
            
            _moveMechanics = new MoveMechanics(_movementDirection, _moveSpeed, _moveCondition, transform);
        }
        
        public void Update()
        {
            _moveMechanics.Update();
        }

        public void Dispose()
        {
            _movementDirection?.Dispose();
        }
    }
}