using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_Core : IDisposable
    {
        [SerializeField]
        private Transform _transform;
        
        [SerializeField]
        private Gun _currentGun;
        
        [SerializeField]
        private HealthComponent _healthComponent;
        
        [SerializeField]
        private MoveComponent _moveComponent;
        
        [SerializeField]
        private RotationComponent _rotationComponent;

        internal IAtomicObservable ShootObservable => _currentGun.ShootObservable;
        
        internal IAtomicValue<int> CurrentHitPoints => _healthComponent.CurrentHitPoints;
        internal IAtomicEvent<int> TakeDamageEvent => _healthComponent.TakeDamageEvent;
        internal IAtomicObservable DeathObservable => _healthComponent.DeathObservable;
        
        internal IAtomicVariable<Vector3> MovementDirection => _moveComponent.MovementDirection;
        internal IAtomicValue<bool> MoveCondition => _moveComponent.MoveCondition;
        
        internal IAtomicVariable<Vector3> RotationDirection => _rotationComponent.RotationDirection;
        
        internal IAtomicValue<bool> AliveCondition => _healthComponent.AliveCondition;
        
        internal void Compose()
        {
            _healthComponent.Compose();
            
            _moveComponent.Let(it =>
            {
                it.Compose(_transform);
                it.MoveCondition.Append(AliveCondition);
            });

            _rotationComponent.Let(it =>
            {
                it.Compose(_transform);
                it.RotationCondition.Append(AliveCondition);
            });
        }
        
        internal void OnEnable()
        {
            _healthComponent.OnEnable();
        }
        
        internal void Update()
        {
            _moveComponent.Update();
            _rotationComponent.Update();
        }
        
        internal void OnDisable()
        {
            _healthComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _healthComponent?.Dispose();
            _moveComponent?.Dispose();
            _rotationComponent?.Dispose();
        }
    }
}