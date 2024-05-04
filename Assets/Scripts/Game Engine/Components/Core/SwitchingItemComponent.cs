using System;
using System.Linq;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class SwitchingItemComponent : IDisposable
    {
        [SerializeField]
        private AtomicVariable<AtomicObject> _currentItem;
        
        private readonly AtomicVariable<int> _currentItemIndex = new(0);

        private SwitchItemMechanics _switchItemMechanics;
        private AssignCurrentItemMechanics _assignCurrentItemMechanics;

        public IAtomicVariable<int> CurrentItemIndex => _currentItemIndex;
        public IAtomicObservable<int> SwitchingItemObservable => _currentItemIndex;
        public IAtomicValue<IAtomicObject> CurrentItem => _currentItem;
        
        public void Compose(AtomicObject[] items)
        {
            GameObject[] itemsGO = items.Select(gun => gun.gameObject).ToArray();
            
            _switchItemMechanics = new SwitchItemMechanics(itemsGO, _currentItemIndex);
            _assignCurrentItemMechanics = new AssignCurrentItemMechanics(items, _currentItemIndex, _currentItem);
        }

        public void OnEnable()
        {
            _switchItemMechanics.OnEnable();
            _assignCurrentItemMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _switchItemMechanics.OnDisable();
            _assignCurrentItemMechanics.OnDisable();
        }

        public void Dispose()
        {
            _currentItemIndex?.Dispose();
            _currentItem?.Dispose();
        }
    }
}