using Common;
using GameCycle;
using JetBrains.Annotations;
using UI.Factories.Reward;
using UI.View;

namespace UI.Managers
{
    [UsedImplicitly]
    internal sealed class RewardManager : IStartGameListener
    {
        private readonly IFactory<RewardView> _rewardViewFactory;
        private readonly IRewardAdapterFactory _rewardAdapterFactory;

        internal RewardManager(IFactory<RewardView> rewardViewFactory, IRewardAdapterFactory rewardAdapterFactory)
        {
            _rewardViewFactory = rewardViewFactory;
            _rewardAdapterFactory = rewardAdapterFactory;
        }

        void IStartGameListener.OnStart()
        {
            RewardView rewardView = _rewardViewFactory.Create();
            _rewardAdapterFactory.Create(rewardView);
        }
    }
}