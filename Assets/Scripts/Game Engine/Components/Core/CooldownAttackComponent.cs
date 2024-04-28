using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class CooldownAttackComponent : IDisposable
    {
        [SerializeField]
        private AtomicValue<float> _attackRange;
        
        private AtomicVariable<Transform> _targetTransform;
        
        private readonly AtomicEvent _attackRequestEvent = new();
        private readonly AtomicEvent<IAtomicObject> _attackEvent = new();
        private readonly AtomicEvent _attackEventNonArgs = new();
        
        private readonly AndExpression _attackCondition = new();

        [SerializeField]
        [HideInInspector]
        private IsInAttackRangeFunction _isInAttackRange;

        [SerializeField] 
        private CooldownComponent _cooldownComponent;
        
        [SerializeField]
        private AttackComponent _attackComponent;
        
        public IAtomicAction<IAtomicObject> AttackAction => _attackEvent;
        
        public IAtomicValue<Transform> TargetTransform => _targetTransform;
        public IAtomicExpression<bool> AttackCondition => _attackCondition;
        public IAtomicObservable AttackRequestEvent => _attackRequestEvent;
        public IAtomicObservable AttackEvent => _attackEventNonArgs;
        
        public void Compose(Transform transform, AtomicVariable<Transform> targetTransform)
        {
            _targetTransform = targetTransform;
            
            _isInAttackRange.Compose(_attackRange, _targetTransform, transform);
            
            _attackCondition.Append(new AtomicFunction<bool>(() => _targetTransform.Value != null));
            _attackCondition.Append(_isInAttackRange);

            _attackEvent.Subscribe(_ => _attackEventNonArgs?.Invoke());

            _cooldownComponent.Let(it =>
            {
                it.Compose(_attackRequestEvent);
                it.CoolDownCondition.Append(_attackCondition);
            });
            
            _attackComponent.Let(it =>
            {
                it.Compose(_attackEvent);
                it.AttackCondition.Append(_attackCondition);
            });
        }

        public void OnEnable()
        {
            _attackComponent.OnEnable();
        }
        
        public void Update()
        {
            _cooldownComponent.Update();
        }
        
        public void OnDisable()
        {
            _attackComponent.OnDisable();
        }

        public void Dispose()
        {
            _targetTransform?.Dispose();
            _attackRequestEvent?.Dispose();
            _attackEvent?.Dispose();
            _attackEventNonArgs?.Dispose();
        }
    }
}