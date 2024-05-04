using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class SwitchItemMechanics
    {
        private readonly GameObject[] _items;
        private readonly IAtomicObservable<int> _switchingItemObservable;

        public SwitchItemMechanics(GameObject[] items, IAtomicObservable<int> switchingItemObservable)
        {
            _items = items;
            _switchingItemObservable = switchingItemObservable;
        }
        
        public void OnEnable()
        {
            _switchingItemObservable.Subscribe(SwitchGun);
        }
        
        public void OnDisable()
        {
            _switchingItemObservable.Unsubscribe(SwitchGun);
        }

        private void SwitchGun(int itemIndex)
        {
            for (int i = 0; i < _items.Length; i++)
                _items[i].SetActive(false);
            
            _items[itemIndex].SetActive(true);
        }
    }
}