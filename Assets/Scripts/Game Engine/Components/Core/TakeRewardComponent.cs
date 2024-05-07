using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine.Components
{
    [Serializable]
    public sealed class TakeRewardComponent
    {
        private readonly AtomicVariable<int> _currentRewardAmount = new(0);
        
        private readonly AtomicAction<IAtomicObject> _takeRewardAction = new();
        
        private readonly AndExpression _aliveCondition = new();
        private readonly TakeRewardCondition _takeRewardCondition = new();
        
        private PassOnTargetMechanics _passOnTargetMechanics;
        
        public IAtomicValueObservable<int> RewardAmount => _currentRewardAmount;

        public IAtomicExpression<bool> AliveCondition => _aliveCondition;
        
        public void Compose()
        {
            _takeRewardAction.Compose(reward =>
            {
                int rewardAmount = reward.GetVariable<int>(RewardAPI.Reward).Value;
                _currentRewardAmount.Value += rewardAmount;
            });
            
            _takeRewardCondition.Compose(_aliveCondition);
            
            _passOnTargetMechanics = new PassOnTargetMechanics(_takeRewardCondition, _takeRewardAction);
        }

        public void OnTriggerEnter(Collider other)
        {
            _passOnTargetMechanics.OnTriggerEnter(other);
        }
    }
}