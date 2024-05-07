using Atomic.Objects;
using GameCycle;
using JetBrains.Annotations;
using UI.Controller;
using UI.View;

namespace UI.Factories.Reward
{
    [UsedImplicitly]
    internal sealed class RewardAdapterFactory : IRewardAdapterFactory
    {
        private readonly GameCycleManager _gameCycleManager;
        private readonly IAtomicObject _character;

        internal RewardAdapterFactory(GameCycleManager gameCycleManager, IAtomicObject character)
        {
            _gameCycleManager = gameCycleManager;
            _character = character;
        }

        RewardAdapter IRewardAdapterFactory.Create(RewardView rewardView)
        {
            RewardAdapter rewardAdapter = new(rewardView, _character);
            _gameCycleManager.AddListener(rewardAdapter);

            return rewardAdapter;
        }
    }
}