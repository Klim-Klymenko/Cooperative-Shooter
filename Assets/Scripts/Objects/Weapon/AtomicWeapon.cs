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
        
        protected abstract IAtomicValue<float> AttackInterval { get; }
        protected abstract IAtomicAction AttackRequestAction { get; }
        
        internal abstract IAtomicObservable AttackRequestObservable { get; }
        internal abstract IAtomicObservable AttackObservable { get; }
    }
}