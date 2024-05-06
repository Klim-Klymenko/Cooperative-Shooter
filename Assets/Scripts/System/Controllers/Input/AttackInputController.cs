using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common;
using Common.LocalInput;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UnityEngine;

namespace System
{
    [UsedImplicitly]
    internal sealed class AttackInputController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicAction _attackAction;
        private Countdown _attackCountdown;
        private Countdown _cooldownCountdown;

        private readonly InputFacade _input;
        private readonly IAtomicObject _attacker;
        
        internal AttackInputController(InputFacade input, IAtomicObject attacker)
        {
            _input = input;
            _attacker = attacker;
        }

        void IStartGameListener.OnStart()
        {
            IAtomicValueObservable<AtomicObject> currentWeapon = _attacker.GetValueObservable<AtomicObject>(WeaponAPI.CurrentWeapon);
            currentWeapon.Subscribe(OnWeaponSwitched);
            
            OnWeaponSwitched(currentWeapon.Value);
        }

        void IUpdateGameListener.OnUpdate()
        {
            _cooldownCountdown.Tick(Time.deltaTime);
            
            if (_input.AttackStartButton)
            {
                if (_cooldownCountdown.IsPlaying()) return;
                
                Attack();
                _cooldownCountdown.Reset();
            }
            
            else if (_input.AttackContinueButton)
            {
                _attackCountdown.Tick(Time.deltaTime);
            
                if (_attackCountdown.IsPlaying()) return;
                
                Attack();
                _attackCountdown.Reset();
            }
            
            else if (_input.AttackStopButton)
                _attackCountdown.Reset();
        }

        private void Attack()
        {
            _attackAction.Invoke();
        }
        
        private void OnWeaponSwitched(IAtomicObject currentWeapon)
        {
            IAtomicValue<float> attackInterval = currentWeapon.GetValue<float>(WeaponAPI.AttackInterval);
            
            _attackAction = currentWeapon.GetAction(WeaponAPI.AttackRequestAction);
            _attackCountdown = new Countdown(attackInterval.Value);
            _cooldownCountdown = new Countdown(attackInterval.Value);
            
            _cooldownCountdown.SetZero();
        }
    }
}