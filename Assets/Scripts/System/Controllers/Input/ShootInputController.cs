using System.Threading.Tasks;
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
    internal sealed class ShootInputController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicAction _shootAction;
        private Countdown _fireCountdown;
        private Countdown _reloadCountdown;

        private readonly InputFacade _input;
        private readonly IAtomicObject _gun;
        private readonly int _rotationToTargetTimeInMilliseconds;
        
        internal ShootInputController(InputFacade input, IAtomicObject gun, int rotationToTargetTimeInMilliseconds)
        {
            _input = input;
            _gun = gun;
            _rotationToTargetTimeInMilliseconds = rotationToTargetTimeInMilliseconds;
        }

        void IStartGameListener.OnStart()
        {
            IAtomicValue<float> shootingInterval = _gun.GetValue<float>(ShooterAPI.ShootingInterval);
            
            _shootAction = _gun.GetAction(ShooterAPI.ShootAction);
            _fireCountdown = new Countdown(shootingInterval.Value);
            _reloadCountdown = new Countdown(shootingInterval.Value);
            
            _reloadCountdown.SetZero();
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

        private async void Shoot()
        {
            await Task.Delay(_rotationToTargetTimeInMilliseconds);
            
            _shootAction.Invoke();
        }
    }
}