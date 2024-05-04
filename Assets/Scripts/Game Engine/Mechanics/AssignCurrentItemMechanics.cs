using Atomic.Elements;
using Atomic.Objects;

namespace GameEngine
{
    public sealed class AssignCurrentItemMechanics
    {
        private readonly AtomicObject[] _items;
        private readonly IAtomicObservable<int> _switchingItemObservable;
        private readonly IAtomicVariable<AtomicObject> _currentItem;

        public AssignCurrentItemMechanics(AtomicObject[] items, IAtomicObservable<int> switchingItemObservable, IAtomicVariable<AtomicObject> currentItem)
        {
            _items = items;
            _switchingItemObservable = switchingItemObservable;
            _currentItem = currentItem;
        }

        public void OnEnable()
        {
            _switchingItemObservable.Subscribe(OnItemSwitched);
        }

        public void OnDisable()
        {
            _switchingItemObservable.Unsubscribe(OnItemSwitched);
        }

        private void OnItemSwitched(int gunIndex)
        {
            _currentItem.Value = _items[gunIndex];
        }
    }
}