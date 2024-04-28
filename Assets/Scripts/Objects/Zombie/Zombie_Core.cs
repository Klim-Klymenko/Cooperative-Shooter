using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Zombie_Core : IDisposable
    {
        [SerializeField]
        private Transform _transform;
        
        [SerializeField] 
        private HealthComponent _healthComponent;
        
        [SerializeField] 
        private CooldownAttackComponent _cooldownAttackComponent;
        
        internal IAtomicValue<int> CurrentHitPoints => _healthComponent.CurrentHitPoints;
        internal IAtomicAction<int> TakeDamageAction => _healthComponent.TakeDamageEvent;
        internal IAtomicObservable DeathObservable => _healthComponent.DeathObservable;
        
        internal IAtomicValue<Transform> TargetTransform => _cooldownAttackComponent.TargetTransform;
        internal IAtomicAction<IAtomicObject> AttackAction => _cooldownAttackComponent.AttackAction;
        
        internal IAtomicObservable AttackRequestEvent => _cooldownAttackComponent.AttackRequestEvent;
        internal IAtomicObservable AttackEvent => _cooldownAttackComponent.AttackEvent;
        internal IAtomicValue<bool> AliveCondition => _healthComponent.AliveCondition;
        
        internal void Compose(AtomicVariable<Transform> targetTransform)
        {
            _healthComponent.Compose();

            _cooldownAttackComponent.Let(it =>
            {
                it.Compose(_transform, targetTransform);
                it.AttackCondition.Append(AliveCondition);
            });
        }
        
        internal void OnEnable()
        {
            _healthComponent.OnEnable();
            _cooldownAttackComponent.OnEnable();
        }
        
        internal void Update()
        {
            _cooldownAttackComponent.Update();
        }

        internal void OnDisable()
        {
            _healthComponent.OnDisable();
            _cooldownAttackComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _healthComponent?.Dispose();
            _cooldownAttackComponent?.Dispose();
        }
    }
}