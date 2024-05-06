using System;
using Atomic.Elements;
using Common;
using GameEngine.Data;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class ShootComponent : IDisposable
    {
        [SerializeField]
        private BulletType _bulletType;
        
        [SerializeField] 
        private AtomicVariable<int> _charges;

        [SerializeField] 
        private AtomicValue<float> _shootingInterval;
        
        private readonly AtomicEvent _shootRequestEvent = new();
        private readonly AtomicEvent _shootEvent = new();
        
        private readonly AtomicAction _shootRequestAction = new();
        private readonly AtomicAction _bulletSpawnAction = new();
        
        private readonly AndExpression _shootCondition = new();
        
        private ShootMechanics _shootMechanics;
        
        public IAtomicValue<float> ShootingInterval => _shootingInterval;
        public IAtomicAction ShootRequestAction => _shootRequestAction;
        public IAtomicAction ShootAction => _shootEvent;
        public IAtomicVariableObservable<int> Charges => _charges;

        public IAtomicObservable ShootRequestObservable => _shootRequestEvent;
        public IAtomicObservable ShootObservable => _shootEvent;
        public IAtomicExpression<bool> ShootCondition => _shootCondition;

        public void Compose(ISpawner<Transform> bulletSpawner)
        {
            _shootCondition.Append(new AtomicFunction<bool>(() => _charges.Value > 0));
            
            _shootRequestAction.Compose(() => { if (_shootCondition.Invoke()) _shootRequestEvent.Invoke(); });
            _bulletSpawnAction.Compose(() => bulletSpawner.Spawn(_bulletType.ToString()));
            
            _shootMechanics = new ShootMechanics(_charges, _bulletSpawnAction, _shootEvent);
        }

        public void OnEnable()
        {
            _shootMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _shootMechanics.OnDisable();
        }
        
        public void Dispose()
        {
            _charges?.Dispose();
            _shootRequestEvent?.Dispose();
            _shootEvent?.Dispose();
        }
    }
}