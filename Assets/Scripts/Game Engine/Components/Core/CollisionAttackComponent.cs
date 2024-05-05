using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class CollisionAttackComponent : IDisposable
    {
        [SerializeField]
        private AttackComponent _attackComponent;

        private readonly AtomicEvent<IAtomicObject> _attackEvent = new();
        private readonly AtomicEvent _attackEventNonArgs = new();
        
        private readonly ZombieTakeDamageCondition _zombieTakeDamageCondition = new();
        private readonly AndExpression _attackCondition = new();
        
        private PassOnTargetMechanics _passOnTargetMechanics;
        
        public IAtomicObservable AttackObservable => _attackEventNonArgs;
        public IAtomicExpression<bool> AttackCondition => _attackCondition;
        
        public void Compose()
        {
            _attackComponent.Let(it =>
            {
                it.Compose(_attackEvent);
                it.AttackCondition.Append(AttackCondition);
            });
            
            _attackEvent.Subscribe(_ => _attackEventNonArgs.Invoke());
            
            _passOnTargetMechanics = new PassOnTargetMechanics(_zombieTakeDamageCondition, _attackEvent);
        }

        public void OnEnable()
        {
            _attackComponent.OnEnable();
        }
        
        public void OnDisable()
        {
            _attackComponent.OnDisable();
        }

        public void OnTriggerEnter(Collider other)
        {
            _passOnTargetMechanics.OnTriggerEnter(other);
        }

        public void Dispose()
        {
            _attackEvent?.Dispose();
            _attackEventNonArgs.Dispose();
        }
    }
}