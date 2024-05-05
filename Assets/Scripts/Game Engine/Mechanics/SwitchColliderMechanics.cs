using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class SwitchColliderMechanics
    {
        private readonly Collider _collider;
        private readonly IAtomicObservable<bool> _switchObservable;

        public SwitchColliderMechanics(Collider collider, IAtomicObservable<bool> switchObservable)
        {
            _collider = collider;
            _switchObservable = switchObservable;
        }

        public void OnEnable()
        {
            _switchObservable.Subscribe(Switch);
        }

        public void OnDisable()
        {
            _switchObservable.Unsubscribe(Switch);
        }
        
        private void Switch(bool value)
        {
            if (_collider.enabled == value) return;
            
            _collider.enabled = value;
        }
    }
}