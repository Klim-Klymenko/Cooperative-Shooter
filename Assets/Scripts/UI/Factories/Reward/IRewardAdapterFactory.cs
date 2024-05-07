using UI.Controller;
using UI.View;

namespace UI.Factories.Reward
{
    internal interface IRewardAdapterFactory
    {
        RewardAdapter Create(RewardView rewardView);
    }
}