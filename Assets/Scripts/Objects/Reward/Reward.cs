using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects.Reward
{
    [Is(TypeAPI.Reward)]
    internal sealed class Reward : AtomicObject, IDisposable
    {
        [Get(RewardAPI.Reward)]
        private readonly AtomicVariable<int> _reward = new();

        [Get(LiveableAPI.DeathObservable)]
        private readonly AtomicEvent _deathEvent = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAtomicObject atomicObject) && atomicObject.Is(TypeAPI.Character))
                _deathEvent.Invoke();
        }

        public void Dispose()
        {
            _reward?.Dispose();
        }
    }
}