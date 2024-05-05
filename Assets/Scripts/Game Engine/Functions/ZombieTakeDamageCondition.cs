using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class ZombieTakeDamageCondition : IAtomicFunction<Component, bool>
    {
        public bool Invoke(Component other)
        {
            if (!other.gameObject.TryGetComponent(out AtomicObject atomicObject)) return false;
            
            return atomicObject.Is(TypeAPI.Damageable) && atomicObject.Is(TypeAPI.Zombie);
        }
    }
}