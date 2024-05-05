using UnityEngine;

namespace GameEngine
{
    public sealed class SwitchComponentMechanics<T> where T : Behaviour
    {
        private readonly T _component;

        public SwitchComponentMechanics(T component)
        {
            _component = component;
        }

        public void OnEnable()
        {
            if (_component.enabled) return;
            
            _component.enabled = true;
        }

        public void OnDisable()
        {
            if (_component == null || !_component.enabled) return;
            
            _component.enabled = false;
        }
    }
}