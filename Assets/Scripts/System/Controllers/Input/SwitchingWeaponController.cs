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
    internal sealed class SwitchingWeaponController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicVariable<int> _currentWeaponIndex;
        private IAtomicValue<bool> _aliveCondition;

        private readonly InputFacade _input;
        private readonly IAtomicObject _attacker;

        internal SwitchingWeaponController(InputFacade input, IAtomicObject attacker)
        {
            _input = input;
            _attacker = attacker;
        }

        void IStartGameListener.OnStart()
        {
            _currentWeaponIndex = _attacker.GetVariable<int>(WeaponAPI.CurrentWeaponIndex);
            _aliveCondition = _attacker.GetValue<bool>(LiveableAPI.AliveCondition);
        }
        
        void IUpdateGameListener.OnUpdate()
        {
            if (!_aliveCondition.Value) return;
            
            if (_input.SwitchingSlot1Button)
                _currentWeaponIndex.Value = 0;
            
            else if (_input.SwitchingSlot2Button)
                _currentWeaponIndex.Value = 1;
            
            else if (_input.SwitchingSlot3Button)
                _currentWeaponIndex.Value = 2;
        }
    }
}