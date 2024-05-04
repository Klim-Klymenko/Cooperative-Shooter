using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public sealed class AttackSoundController
    {
        private readonly AudioSource _audioSource;
        private readonly IAtomicValue<IAtomicObject> _currentWeapon;
        private readonly IAtomicObservable _attackObservable;

        public AttackSoundController(AudioSource audioSource, IAtomicValue<IAtomicObject> currentWeapon, IAtomicObservable attackObservable)
        {
            _audioSource = audioSource;
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
            AudioClip attackClip = _currentWeapon.Value.Get<AudioClip>(SoundAPI.AttackClip);
            _audioSource.PlayOneShot(attackClip);
        }
    }
}