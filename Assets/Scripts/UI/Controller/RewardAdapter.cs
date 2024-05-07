using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UI.View;

namespace UI.Controller
{
    [UsedImplicitly]
    internal sealed class RewardAdapter : IStartGameListener, IFinishGameListener
    {
        private IAtomicValueObservable<int> _rewardAmount;
        
        private readonly RewardView _rewardView;
        private readonly IAtomicObject _character;

        internal RewardAdapter(RewardView rewardView, IAtomicObject character)
        {
            _rewardView = rewardView;
            _character = character;
        }

        void IStartGameListener.OnStart()
        {
            _rewardAmount = _character.GetValueObservable<int>(RewardAPI.RewardAmount);
            _rewardAmount.Subscribe(ChangeReward);
            ChangeReward(_rewardAmount.Value);
        }

        void IFinishGameListener.OnFinish()
        {
            _rewardAmount.Unsubscribe(ChangeReward);
        }

        private void ChangeReward(int rewardAmount)
        {
            _rewardView.ChangeRewardText(rewardAmount.ToString());
        }
    }
}