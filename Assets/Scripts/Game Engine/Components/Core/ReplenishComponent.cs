using System;
using Atomic.Elements;
using Atomic.Extensions;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class ReplenishComponent : IDisposable
    {
        [SerializeField] 
        private CooldownComponent _cooldownComponent;

        [SerializeField] 
        private GameObject _gameObject;
        
        private readonly AtomicEvent _replenishEvent = new();
        private readonly AndExpression _replenishCondition = new();
        
        private ReplenishMechanics _replenishMechanics;

        public IAtomicExpression<bool> ReplenishCondition => _replenishCondition;
        
        public void Compose(IAtomicVariable<int> charges)
        {
            _replenishCondition.Append(new AtomicFunction<bool>(() => _gameObject.activeInHierarchy));
            
            _cooldownComponent.Let(it =>
            {
                it.Compose(_replenishEvent);
                it.CoolDownCondition.Append(_replenishCondition);
            });
            
            _replenishMechanics = new ReplenishMechanics(_replenishEvent, charges, _replenishCondition);
        }
        
        public void OnEnable()
        {
            _replenishMechanics.OnEnable();
        }
        
        public void Update()
        {
            _cooldownComponent.Update();
        }
        
        public void OnDisable()
        {
            _replenishMechanics.OnDisable();
        }

        public void Dispose()
        {
            _replenishEvent?.Dispose();
        }
    }
}