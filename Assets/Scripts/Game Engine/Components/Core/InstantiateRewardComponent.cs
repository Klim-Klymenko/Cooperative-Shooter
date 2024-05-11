using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine.Interfaces;
using UnityEngine;

namespace GameEngine.Components
{
    [Serializable]
    public sealed class InstantiateRewardComponent
    {
        [SerializeField] 
        private AtomicValue<Transform> _rewardSpawnPoint;
        
        [SerializeField]
        private AtomicValue<int> _rewardAmount;
        
        public void Compose(IRewardSpawner rewardSpawner, IAtomicObservable spawnObservable)
        {
            AtomicAction spawnRewardAction = new(() =>
            {
                AtomicObject atomicObject = rewardSpawner.Spawn(_rewardSpawnPoint.Value);
                atomicObject.GetVariable<int>(RewardAPI.Reward).Value = _rewardAmount.Value;
            });
            
            spawnObservable.Subscribe(() => spawnRewardAction.Invoke());
        }
    }
}