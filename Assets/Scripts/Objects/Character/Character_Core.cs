using System;
using System.Linq;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using GameEngine.Components;
using GameEngine.Components.Core;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_Core : IDisposable
    {
        [SerializeField]
        private Transform _transform;
      
        [SerializeField]
        private HealthComponent _healthComponent;
        
        [SerializeField]
        private MoveComponent _moveComponent;
        
        [SerializeField]
        private RotationComponent _rotationComponent;
        
        [SerializeField]
        private WeaponComponent _weaponComponent;
        
        [SerializeField]
        private SwitchingItemComponent _switchingItemComponent;
        
        [SerializeField]
        private TakeRewardComponent _takeRewardComponent;

        [SerializeField] 
        private SwapItemComponent _swapItemComponent;
        
        internal IAtomicVariableObservable<int> HitPoints => _healthComponent.HitPoints;
        internal IAtomicEvent<int> TakeDamageEvent => _healthComponent.TakeDamageEvent;
        internal IAtomicObservable DeathObservable => _healthComponent.DeathObservable;
        internal IAtomicVariable<Vector3> MovementDirection => _moveComponent.MovementDirection;
        internal IAtomicValue<bool> MoveCondition => _moveComponent.MoveCondition;
        internal IAtomicVariable<Vector3> RotationDirection => _rotationComponent.RotationDirection;
        internal IAtomicVariable<int> CurrentWeaponIndex => _switchingItemComponent.CurrentItemIndex;
        internal IAtomicValueObservable<AtomicObject> CurrentWeapon => _switchingItemComponent.CurrentItem;
        internal AtomicObject[] Weapons => _switchingItemComponent.AtomicItems;
        internal IAtomicVariableObservable<int> RewardAmount => _takeRewardComponent.RewardAmount;
        internal IAtomicAction<int, AtomicObject> SwapWeaponAction => _swapItemComponent.SwapItemEvent;
        
        internal IAtomicValue<bool> AliveCondition => _healthComponent.AliveCondition;
        internal IAtomicObservable AttackRequestObservable => _weaponComponent.AttackRequestObservable;
        internal IAtomicObservable AttackObservable => _weaponComponent.AttackObservable;
        
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

            _weaponComponent.Compose();
            _switchingItemComponent.Compose();

            _swapItemComponent.Let(it =>
            {
                it.Compose(_switchingItemComponent.AtomicItems, _switchingItemComponent.ItemsGO, _switchingItemComponent.CurrentItem, CurrentWeaponIndex);
                it.SwapItemCondition.Append(AliveCondition);
            });
           
            _takeRewardComponent.Let(it =>
            {
                it.Compose();
                it.AliveCondition.Append(AliveCondition);
            });
        }
        
        internal void OnEnable()
        {
            _healthComponent.OnEnable();
            _switchingItemComponent.OnEnable();
            _swapItemComponent.OnEnable();
        }
        
        internal void Update()
        {
            _moveComponent.Update();
            _rotationComponent.Update();
        }
        
        internal void OnTriggerEnter(Collider other)
        {
            _takeRewardComponent.OnTriggerEnter(other);
        }
        
        internal void OnDisable()
        {
            _healthComponent.OnDisable();
            _switchingItemComponent.OnDisable();
            _swapItemComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _healthComponent?.Dispose();
            _moveComponent?.Dispose();
            _rotationComponent?.Dispose();
            _switchingItemComponent.Dispose();
            _takeRewardComponent.Dispose();
            _swapItemComponent.Dispose();
        }
    }
}