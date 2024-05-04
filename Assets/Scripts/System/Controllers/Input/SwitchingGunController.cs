using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common.LocalInput;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;

namespace System
{
    [UsedImplicitly]
    internal sealed class SwitchingGunController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicVariable<int> _currentGunIndex;

        private readonly InputFacade _input;
        private readonly IAtomicObject _shooter;

        internal SwitchingGunController(InputFacade input, IAtomicObject shooter)
        {
            _input = input;
            _shooter = shooter;
        }

        void IStartGameListener.OnStart()
        {
            _currentGunIndex = _shooter.GetVariable<int>(WeaponAPI.CurrentWeaponIndex);
        }
        
        void IUpdateGameListener.OnUpdate()
        {
            if (_input.SwitchingSlot1Button)
                _currentGunIndex.Value = 0;
            
            else if (_input.SwitchingSlot2Button)
                _currentGunIndex.Value = 1;
            
            else if (_input.SwitchingSlot3Button)
                _currentGunIndex.Value = 2;
        }
    }
}