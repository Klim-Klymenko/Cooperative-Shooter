using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine.Components.Core
{
    [Serializable]
    public sealed class SwapItemComponent : IDisposable
    {
        private readonly AtomicEvent<int, AtomicObject> _swapItemEvent = new();
        private readonly AndExpression _swapItemCondition = new();

        private SwapCurrentItemMechanics _swapCurrentItemMechanics;
        private SwapItemMechanics _swapItemMechanics;
        
        public IAtomicEvent<int, AtomicObject> SwapItemEvent => _swapItemEvent;
        
        public IAtomicExpression<bool> SwapItemCondition => _swapItemCondition;

        public void Compose(AtomicObject[] atomicItems, GameObject[] itemsGO, 
            IAtomicVariable<AtomicObject> currentItem, IAtomicValue<int> currentItemIndex)
        {
            _swapCurrentItemMechanics = new SwapCurrentItemMechanics(_swapItemEvent, currentItem, currentItemIndex, _swapItemCondition);
            _swapItemMechanics = new SwapItemMechanics(_swapItemEvent, atomicItems, itemsGO, _swapItemCondition);
        }

        public void OnEnable()
        {
            _swapCurrentItemMechanics.OnEnable();
            _swapItemMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _swapCurrentItemMechanics.OnDisable();
            _swapItemMechanics.OnDisable();
        }

        public void Dispose()
        {
            _swapItemEvent?.Dispose();
        }
    }
}