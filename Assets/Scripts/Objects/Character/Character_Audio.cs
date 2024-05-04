using System;
using Atomic.Elements;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_Audio : IDisposable
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioSource _moveAudioSource;
        
        [SerializeField] 
        private AudioClip _takeDamageClip;

        [SerializeField]
        private AudioClip _deathClip;
        
        [SerializeField]
        private AudioClip _moveClip;
        
        private readonly AtomicEvent _moveClipPlayEvent = new();
        private readonly AtomicEvent _moveClipStopEvent = new();

        private AttackSoundController _attackSoundController;
        private TakeDamageSoundController _takeDamageSoundController;
        private DeathSoundController _deathSoundController;
        
        private MoveTimeController _moveTimeSoundController;
        
        public void Compose(Character_Core core)
        {
            AtomicValue<float> moveClipDuration = new(_moveClip.length);
            
            _attackSoundController = new AttackSoundController(_audioSource, core.CurrentGun, core.AttackObservable);
            _takeDamageSoundController = new TakeDamageSoundController(core.TakeDamageEvent, core.AliveCondition, _audioSource, _takeDamageClip);
            _deathSoundController = new DeathSoundController(core.DeathObservable, _audioSource, _deathClip);

            _moveTimeSoundController = new MoveTimeController(core.MoveCondition, moveClipDuration, _moveClipPlayEvent, _moveClipStopEvent);
            _moveAudioSource.clip = _moveClip;
            _moveClipPlayEvent.Subscribe(() => _moveAudioSource.Play());
            _moveClipStopEvent.Subscribe(() => _moveAudioSource.Stop());
        }
        
        public void OnEnable()
        {
            _attackSoundController.OnEnable();
            _takeDamageSoundController.OnEnable();
            _deathSoundController.OnEnable();
        }
        
        public void Update()
        {
            _moveTimeSoundController.Update();
        }
        
        public void OnDisable()
        {
            _attackSoundController.OnDisable();
            _takeDamageSoundController.OnDisable();
            _deathSoundController.OnDisable();
        }

        public void Dispose()
        {
            _moveClipPlayEvent?.Dispose();
            _moveClipStopEvent?.Dispose();
        }
    }
}