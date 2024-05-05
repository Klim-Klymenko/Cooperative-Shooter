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
        private IAtomicValue<IAtomicObject> _currentWeapon;
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
            IAtomicObservable<int> switchingWeaponObservable = _attacker.GetObservable<int>(WeaponAPI.SwitchingWeaponObservable);
            switchingWeaponObservable.Subscribe(OnWeaponSwitched);

            _currentWeapon = _attacker.GetValue<IAtomicObject>(WeaponAPI.CurrentWeapon);
            
            OnWeaponSwitched(0);
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
        
        private void OnWeaponSwitched(int _)
        {
            IAtomicValue<float> attackInterval = _currentWeapon.Value.GetValue<float>(WeaponAPI.AttackInterval);
            
            _attackAction = _currentWeapon.Value.GetAction(WeaponAPI.AttackRequestAction);
            _attackCountdown = new Countdown(attackInterval.Value);
            _cooldownCountdown = new Countdown(attackInterval.Value);
            
            _cooldownCountdown.SetZero();
        }
    }
}