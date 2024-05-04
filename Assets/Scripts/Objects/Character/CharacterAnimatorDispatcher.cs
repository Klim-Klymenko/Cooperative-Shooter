using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;

namespace Objects
{
    internal sealed class CharacterAnimatorDispatcher : MonoBehaviour, IStartGameListener
    {
        [SerializeField]
        private AtomicObject _character;

        private IAtomicValue<IAtomicObject> _currentWeapon;
        
        void IStartGameListener.OnStart()
        {
            _currentWeapon = _character.GetValue<IAtomicObject>(WeaponAPI.CurrentWeapon);
        }

        public void OnAttack()
        {
            _currentWeapon.Value.InvokeAction(WeaponAPI.AttackAction);
        }
    }
}