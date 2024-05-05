using System;
using Atomic.Elements;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_Particle : IDisposable
    {
        [SerializeField]
        private ParticleSystem _damageParticle;
        
        [SerializeField]
        private ParticleSystem _moveParticle;
        
        private readonly AtomicEvent _moveParticlePlayEvent = new();
        private readonly AtomicEvent _moveParticleStopEvent = new();

        private AttackParticleController _attackParticleController;
        
        private MoveTimeController _moveTimeParticleController;
        
        public void Compose(Character_Core core)
        {
            AtomicValue<float> moveParticleDuration = new(_moveParticle.main.duration);

            _attackParticleController = new AttackParticleController(core.CurrentWeapon, core.AttackObservable);
            
            core.TakeDamageEvent.Subscribe(_ => _damageParticle.Play(withChildren: true));
            
            _moveTimeParticleController = new MoveTimeController(core.MoveCondition, moveParticleDuration, _moveParticlePlayEvent, _moveParticleStopEvent);
            _moveParticlePlayEvent.Subscribe(() => _moveParticle.Play());
            _moveParticleStopEvent.Subscribe(() => _moveParticle.Stop());
        }

        public void OnEnable()
        {
            _attackParticleController.OnEnable();
        }
        
        public void Update()
        {
            _moveTimeParticleController.Update();
        }

        public void OnDisable()
        {
            _attackParticleController.OnDisable();
        }
        
        public void Dispose()
        {
            _moveParticlePlayEvent?.Dispose();
            _moveParticleStopEvent?.Dispose();
        }
    }
}