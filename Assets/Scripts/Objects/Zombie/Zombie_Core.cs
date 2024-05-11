﻿using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common;
using GameEngine;
using GameEngine.Components;
using GameEngine.Interfaces;
using Objects.Reward;
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

        [SerializeField]
        private InstantiateRewardComponent _instantiateRewardComponent;
        
        internal IAtomicEvent<int> TakeDamageEvent => _healthComponent.TakeDamageEvent;
        internal IAtomicObservable DeathObservable => _healthComponent.DeathObservable;
        
        internal IAtomicValue<Transform> TargetTransform => _cooldownAttackComponent.TargetTransform;
        internal IAtomicAction<IAtomicObject> AttackAction => _cooldownAttackComponent.AttackAction;
        
        internal IAtomicObservable AttackRequestEvent => _cooldownAttackComponent.AttackRequestEvent;
        internal IAtomicObservable AttackEvent => _cooldownAttackComponent.AttackEvent;
        internal IAtomicValue<bool> AliveCondition => _healthComponent.AliveCondition;
        
        internal void Compose(AtomicVariable<Transform> targetTransform, IRewardSpawner rewardSpawner)
        {
            _healthComponent.Compose();

            _cooldownAttackComponent.Let(it =>
            {
                it.Compose(_transform, targetTransform);
                it.AttackCondition.Append(AliveCondition);
            });
            
            _instantiateRewardComponent.Compose(rewardSpawner, DeathObservable);
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