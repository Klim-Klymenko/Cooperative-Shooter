using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public static class WeaponAPI
    {
        [Contract(typeof(IAtomicAction))]
        public const string AttackRequestAction = nameof(AttackRequestAction);
        
        [Contract(typeof(IAtomicAction))]
        public const string AttackAction = nameof(AttackAction);

        [Contract(typeof(IAtomicValue<int>))] 
        public const string AttackInterval = nameof(AttackInterval);
        
        [Contract(typeof(IAtomicVariable<int>))]
        public const string CurrentWeaponIndex = nameof(CurrentWeaponIndex);
        
        [Contract(typeof(IAtomicValueObservable<AtomicObject>))]
        public const string CurrentWeapon = nameof(CurrentWeapon);

        [Contract(typeof(IAtomicVariableObservable<int>))]
        public const string Charges = nameof(Charges);
        
        [Contract(typeof(Sprite))]
        public const string WeaponSprite = nameof(WeaponSprite);
    }
}