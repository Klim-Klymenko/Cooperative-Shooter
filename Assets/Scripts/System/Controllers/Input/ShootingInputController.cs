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
    internal sealed class ShootingInputController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicValue<IAtomicObject> _currentGun;
        private IAtomicAction _shootAction;
        private Countdown _fireCountdown;
        private Countdown _reloadCountdown;

        private readonly InputFacade _input;
        private readonly IAtomicObject _shooter;
        
        internal ShootingInputController(InputFacade input, IAtomicObject shooter)
        {
            _input = input;
            _shooter = shooter;
        }

        void IStartGameListener.OnStart()
        {
            IAtomicObservable<int> switchingGunObservable = _shooter.GetObservable<int>(WeaponAPI.SwitchingWeaponObservable);
            switchingGunObservable.Subscribe(OnGunSwitched);

            _currentGun = _shooter.GetValue<IAtomicObject>(WeaponAPI.CurrentWeapon);
            
            OnGunSwitched(0);
        }

        void IUpdateGameListener.OnUpdate()
        {
            _reloadCountdown.Tick(Time.deltaTime);
            
            if (_input.ShootingStartButton)
            {
                if (_reloadCountdown.IsPlaying()) return;
                
                Shoot();
                _reloadCountdown.Reset();
            }
            
            else if (_input.ShootingContinueButton)
            {
                _fireCountdown.Tick(Time.deltaTime);
            
                if (_fireCountdown.IsPlaying()) return;
                
                Shoot();
                _fireCountdown.Reset();
            }
            
            else if (_input.ShootingStopButton)
                _fireCountdown.Reset();
        }

        private void Shoot()
        {
            _shootAction.Invoke();
        }
        
        private void OnGunSwitched(int _)
        {
            IAtomicValue<float> shootingInterval = _currentGun.Value.GetValue<float>(WeaponAPI.AttackInterval);
            
            _shootAction = _currentGun.Value.GetAction(WeaponAPI.AttackRequestAction);
            _fireCountdown = new Countdown(shootingInterval.Value);
            _reloadCountdown = new Countdown(shootingInterval.Value);
            
            _reloadCountdown.SetZero();
        }
    }
}