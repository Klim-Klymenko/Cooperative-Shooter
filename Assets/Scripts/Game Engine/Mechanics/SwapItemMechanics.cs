using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public sealed class SwapItemMechanics
    {
        private readonly IAtomicObservable<int, AtomicObject> _swapItemObservable;
        private readonly AtomicObject[] _atomicItems;
        private readonly GameObject[] _itemsGO;
        private readonly IAtomicValue<bool> _swapCondition;

        public SwapItemMechanics(IAtomicObservable<int, AtomicObject> swapItemObservable, AtomicObject[] atomicItems, 
            GameObject[] itemsGO, IAtomicValue<bool> swapCondition)
        {
            _swapItemObservable = swapItemObservable;
            _atomicItems = atomicItems;
            _itemsGO = itemsGO;
            _swapCondition = swapCondition;
        }

        public void OnEnable()
        {
            _swapItemObservable.Subscribe(SwapItem);
        }

        public void OnDisable()
        {
            _swapItemObservable.Unsubscribe(SwapItem);
        }

        private void SwapItem(int index, AtomicObject swappedItem)
        {
            if (!_swapCondition.Value) return;
            
            _itemsGO[index] = swappedItem.gameObject;
            _atomicItems[index] = swappedItem;
        }
    }
}