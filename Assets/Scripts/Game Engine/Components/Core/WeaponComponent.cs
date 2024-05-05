using System;
using System.Collections.Generic;
using Atomic.Elements;
using Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class WeaponComponent
    {
        [SerializeField]
        private AtomicWeapon[] _weapons;
        
        private readonly AtomicEvent _attackRequestEvent = new();
        private readonly AtomicEvent _attackEvent = new();
        
        public IReadOnlyList<AtomicWeapon> Weapons => _weapons;
        public IAtomicObservable AttackRequestObservable => _attackRequestEvent;
        public IAtomicObservable AttackObservable => _attackEvent;

        public void Compose()
        {
            for (int i = 0; i < _weapons.Length; i++)
                _weapons[i].AttackRequestObservable.Subscribe(() => _attackRequestEvent.Invoke());
            
            for (int i = 0; i < _weapons.Length; i++)
                _weapons[i].AttackObservable.Subscribe(() => _attackEvent.Invoke());
        }
    }
}