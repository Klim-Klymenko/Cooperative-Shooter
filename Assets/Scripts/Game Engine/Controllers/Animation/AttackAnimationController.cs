using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class AttackAnimationController
    {
        private static readonly int _attackTrigger = Animator.StringToHash("Attack");
        private static readonly int _weaponSlotInteger = Animator.StringToHash("Current Weapon Slot");
        
        private const int ArrayIndexOffset = 1;
        
        private readonly Animator _animator;
        private readonly IAtomicObservable _attackObservable;
        private readonly IAtomicValue<int> _currentWeaponSlot;

        public AttackAnimationController(Animator animator, IAtomicObservable attackObservable, IAtomicValue<int> currentWeaponSlot = null)
        {
            _animator = animator;
            _attackObservable = attackObservable;
            _currentWeaponSlot = currentWeaponSlot;
        }
        
        public void OnEnable()
        {
            _attackObservable.Subscribe(OnShoot);
        }
        
        public void OnDisable()
        {
            _attackObservable.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            _animator.SetTrigger(_attackTrigger);
            
            if (_currentWeaponSlot != null)
                _animator.SetInteger(_weaponSlotInteger, _currentWeaponSlot.Value + ArrayIndexOffset);
        }
    }
}