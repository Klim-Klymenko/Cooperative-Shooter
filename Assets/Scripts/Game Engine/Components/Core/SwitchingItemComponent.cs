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
        private GameObject[] _items;
        
        [SerializeField]
        private AtomicObject[] _atomicItems;
        
        [SerializeField]
        private AtomicVariable<AtomicObject> _currentItem;
        
        private readonly AtomicVariable<int> _currentItemIndex = new(0);
        
        private SwitchItemMechanics _switchItemMechanics;
        private AssignCurrentItemMechanics _assignCurrentItemMechanics;

        public GameObject[] ItemsGO => _items;
        public AtomicObject[] AtomicItems => _atomicItems;
        
        public IAtomicVariable<int> CurrentItemIndex => _currentItemIndex;
        public IAtomicVariableObservable<AtomicObject> CurrentItem => _currentItem;

        public void Compose()
        {
            _switchItemMechanics = new SwitchItemMechanics(_items, _currentItemIndex);
            _assignCurrentItemMechanics = new AssignCurrentItemMechanics(_atomicItems, _currentItemIndex, _currentItem);
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