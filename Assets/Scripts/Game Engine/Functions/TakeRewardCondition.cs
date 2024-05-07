using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class TakeRewardCondition : IAtomicFunction<Component, bool>
    {
        private IAtomicValue<bool> _aliveCondition;

        public void Compose(IAtomicValue<bool> aliveCondition)
        {
            _aliveCondition = aliveCondition;
        }

        bool IAtomicFunction<Component, bool>.Invoke(Component other)
        {
            if (_aliveCondition.Value == false) return false; 
            
            return other.TryGetComponent(out IAtomicObject atomicObject) && atomicObject.Is(TypeAPI.Reward);
        }
    }
}