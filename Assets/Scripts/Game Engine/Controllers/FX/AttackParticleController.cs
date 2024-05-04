using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public sealed class AttackParticleController
    {
        private readonly IAtomicValue<IAtomicObject> _currentWeapon;
        private readonly IAtomicObservable _attackObservable;

        public AttackParticleController(IAtomicValue<IAtomicObject> currentWeapon, IAtomicObservable attackObservable)
        {
            _currentWeapon = currentWeapon;
            _attackObservable = attackObservable;
        }

        public void OnEnable()
        {
            _attackObservable.Subscribe(OnAttack);
        }
        
        public void OnDisable()
        {
            _attackObservable.Unsubscribe(OnAttack);
        }

        private void OnAttack()
        {
            if (_currentWeapon.Value.TryGet(ParticleAPI.AttackParticle, out ParticleSystem attackParticle) && attackParticle != null)
                attackParticle.Play(withChildren: true);
        }
    }
}