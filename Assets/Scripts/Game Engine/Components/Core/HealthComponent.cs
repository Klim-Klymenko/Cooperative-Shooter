using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class HealthComponent : IDisposable
    {
        [SerializeField]
        private int _hitPoints;
        
        private AtomicVariable<int> _currentHitPoints = new();
        
        private readonly AtomicEvent<int> _takeDamageEvent = new();
        private readonly AtomicEvent _deathEvent = new();
        
        private readonly AtomicFunction<bool> _aliveCondition = new();
        
        private TakeDamageMechanics _takeDamageMechanics;
        private DeathMechanics _deathMechanics;
        
        public IAtomicVariableObservable<int> HitPoints => _currentHitPoints;
        public IAtomicEvent<int> TakeDamageEvent => _takeDamageEvent;
        public IAtomicObservable DeathObservable => _deathEvent;
        
        public IAtomicValue<bool> AliveCondition => _aliveCondition;
        
        public void Compose()
        {
            _currentHitPoints.Value = _hitPoints;
            
            _aliveCondition.Compose(() => _currentHitPoints.Value > 0);
            
            _takeDamageMechanics = new TakeDamageMechanics(_currentHitPoints, _takeDamageEvent, _aliveCondition);
            _deathMechanics = new DeathMechanics(_currentHitPoints, _deathEvent);
        }
        
        public void OnEnable()
        {
            _takeDamageMechanics.OnEnable();
            _deathMechanics.OnEnable();
        }
        
        public void OnDisable()
        {
            _takeDamageMechanics.OnDisable();
            _deathMechanics.OnDisable();
        }

        public void Dispose()
        {
            _currentHitPoints?.Dispose();
            _takeDamageEvent?.Dispose();
            _deathEvent?.Dispose();
        }
    }
}