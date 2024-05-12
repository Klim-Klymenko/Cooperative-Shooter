using Atomic.Elements;
using Atomic.Objects;

namespace GameEngine
{
    public sealed class SwapCurrentItemMechanics
    {
        private readonly IAtomicObservable<int, AtomicObject> _swapItemObservable;
        private readonly IAtomicVariable<AtomicObject> _currentItem;
        private readonly IAtomicValue<int> _currentItemIndex;
        private readonly IAtomicValue<bool> _swapCondition;

        public SwapCurrentItemMechanics(IAtomicObservable<int, AtomicObject> swapItemObservable,
            IAtomicVariable<AtomicObject> currentItem, IAtomicValue<int> currentItemIndex, IAtomicValue<bool> swapCondition)
        {
            _swapItemObservable = swapItemObservable;
            _currentItem = currentItem;
            _currentItemIndex = currentItemIndex;
            _swapCondition = swapCondition;
        }

        public void OnEnable()
        {
            _swapItemObservable.Subscribe(OnItemSwapped);
        }

        public void OnDisable()
        {
            _swapItemObservable.Unsubscribe(OnItemSwapped);
        }
        
        private void OnItemSwapped(int index, AtomicObject swappedItem)
        {
            if (!_swapCondition.Value) return;
            if (_currentItemIndex.Value != index) return;
                
            _currentItem.Value.gameObject.SetActive(false);
            swappedItem.gameObject.SetActive(true);
                
            _currentItem.Value = swappedItem;
        }
    }
}