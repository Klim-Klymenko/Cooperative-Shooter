using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveAnimationController
    {
        private static readonly int _moveInteger = Animator.StringToHash("Speed");
        private static readonly int _weaponSlotInteger = Animator.StringToHash("Current Weapon Slot");
        
        private const int IdleSpeed = 0;
        private const int RunSpeed = 1;

        private const int ArrayIndexOffset = 1;
        
        private readonly Animator _animator;
        private readonly IAtomicValue<bool> _moveCondition;
        private readonly IAtomicValue<int> _currentWeaponSlot;

        public MoveAnimationController(Animator animator, IAtomicValue<bool> moveCondition, IAtomicValue<int> currentWeaponSlot = null)
        {
            _animator = animator;
            _moveCondition = moveCondition;
            _currentWeaponSlot = currentWeaponSlot;
        }

        public void Update()
        {
            OnMove();
        }
        
        private void OnMove()
        {
            if (_moveCondition.Value)
            {
                _animator.SetInteger(_moveInteger, RunSpeed);

                if (_currentWeaponSlot != null)
                    _animator.SetInteger(_weaponSlotInteger, _currentWeaponSlot.Value + ArrayIndexOffset);
            }
            else
                _animator.SetInteger(_moveInteger, IdleSpeed);
        }
    }
}