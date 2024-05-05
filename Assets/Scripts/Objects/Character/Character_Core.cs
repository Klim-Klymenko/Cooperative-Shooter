using System;
using System.Linq;
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
        private HealthComponent _healthComponent;
        
        [SerializeField]
        private MoveComponent _moveComponent;
        
        [SerializeField]
        private RotationComponent _rotationComponent;
        
        [SerializeField]
        private WeaponComponent _weaponComponent;
        
        [SerializeField]
        private SwitchingItemComponent _switchingItemComponent;
        
        internal IAtomicValue<int> CurrentHitPoints => _healthComponent.CurrentHitPoints;
        internal IAtomicEvent<int> TakeDamageEvent => _healthComponent.TakeDamageEvent;
        internal IAtomicObservable DeathObservable => _healthComponent.DeathObservable;
        internal IAtomicVariable<Vector3> MovementDirection => _moveComponent.MovementDirection;
        internal IAtomicValue<bool> MoveCondition => _moveComponent.MoveCondition;
        internal IAtomicVariable<Vector3> RotationDirection => _rotationComponent.RotationDirection;
        internal IAtomicVariable<int> CurrentWeaponIndex => _switchingItemComponent.CurrentItemIndex;
        internal IAtomicObservable<int> SwitchingWeaponObservable => _switchingItemComponent.SwitchingItemObservable;
        internal IAtomicValue<IAtomicObject> CurrentWeapon => _switchingItemComponent.CurrentItem;
        
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
            
            AtomicObject[] weapons = _weaponComponent.Weapons.Select(it => it as AtomicObject).ToArray();
            _switchingItemComponent.Compose(weapons);
        }
        
        internal void OnEnable()
        {
            _healthComponent.OnEnable();
            _switchingItemComponent.OnEnable();
        }
        
        internal void Update()
        {
            _moveComponent.Update();
            _rotationComponent.Update();
        }
        
        internal void OnDisable()
        {
            _healthComponent.OnDisable();
            _switchingItemComponent.OnDisable();
        }
        
        public void Dispose()
        {
            _healthComponent?.Dispose();
            _moveComponent?.Dispose();
            _rotationComponent?.Dispose();
            _switchingItemComponent.Dispose();
        }
    }
}