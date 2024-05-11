using Atomic.Objects;
using UnityEngine;

namespace GameEngine.Interfaces
{
    public interface IRewardSpawner
    {
        AtomicObject Spawn(Transform spawnPoint);
        void Despawn(AtomicObject reward);
    }
}