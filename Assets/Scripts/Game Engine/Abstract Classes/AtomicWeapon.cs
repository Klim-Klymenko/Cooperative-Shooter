using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    public abstract class AtomicWeapon : AtomicObject
    {
        [Get(SoundAPI.AttackClip)]
        [SerializeField]
        private AudioClip _attackClip;
        
        [Get(WeaponAPI.WeaponSprite)]
        [SerializeField]
        private Sprite _weaponSprite;
        
        protected abstract IAtomicValue<float> AttackInterval { get; }
        protected abstract IAtomicAction AttackRequestAction { get; }
        
        public abstract IAtomicObservable AttackRequestObservable { get; }
        public abstract IAtomicObservable AttackObservable { get; }
    }
}