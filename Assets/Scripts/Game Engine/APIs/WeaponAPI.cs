using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;

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
        
        [Contract(typeof(IAtomicObservable<int>))]
        public const string SwitchingWeaponObservable = nameof(SwitchingWeaponObservable);
        
        [Contract(typeof(IAtomicValue<IAtomicObject>))]
        public const string CurrentWeapon = nameof(CurrentWeapon);
    }
}