using System;
using Atomic.Elements;
using Common;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class ShootComponent : IDisposable
    {
        [SerializeField] 
        private Transform _firePoint;

        [SerializeField] 
        private AtomicVariable<int> _charges;

        [SerializeField] 
        private AtomicValue<float> _shootingInterval;
        
        private readonly AtomicEvent _shootEvent = new();
        private readonly AndExpression _shootCondition = new();
        
        [SerializeField]
        [HideInInspector]
        private AtomicAction _bulletSpawnAction;

        [SerializeField] 
        [HideInInspector]
        private ShootAction _shootAction;

        public IAtomicValue<float> ShootingInterval => _shootingInterval;
        public IAtomicAction ShootAction => _shootAction;
        
        public IAtomicVariable<int> Charges => _charges;
        public IAtomicObservable ShootObservable => _shootEvent;
        public IAtomicExpression<bool> ShootCondition => _shootCondition;

        public void Compose(ISpawner<Transform> bulletSpawner)
        {
            _bulletSpawnAction.Compose(() => bulletSpawner.Spawn());
            
            _shootCondition.Append(new AtomicFunction<bool>(() => _charges.Value > 0));

            _shootAction.Compose(_charges, _bulletSpawnAction, _shootEvent, _shootCondition);
        }
        
        public void Dispose()
        {
            _charges?.Dispose();
            _shootEvent?.Dispose();
        }
    }
}