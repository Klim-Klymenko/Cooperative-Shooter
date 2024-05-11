using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common;
using GameEngine;
using GameEngine.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Objects.Reward
{
    [UsedImplicitly]
    internal class RewardSpawner : IRewardSpawner
    {
        private readonly Pool<Reward> _pool;
        
        internal RewardSpawner(Pool<Reward> pool)
        {
            _pool = pool;
        }
        
        AtomicObject IRewardSpawner.Spawn(Transform spawnPoint)
        {
            Reward reward = _pool.Get();
            
            reward.Compose();
            
            IAtomicObservable deathObservable = reward.GetObservable(LiveableAPI.DeathObservable);
            deathObservable.Subscribe(() => Despawn(reward));
          
            reward.transform.position = spawnPoint.position;

            return reward;
        }

        public void Despawn(AtomicObject atomicObject)
        {
            Reward reward = atomicObject.GetComponent<Reward>();
            reward.Dispose();
            
            _pool.Put(reward);
        }
    }
}