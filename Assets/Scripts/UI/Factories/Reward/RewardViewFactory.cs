using Common;
using JetBrains.Annotations;
using UI.View;
using UnityEngine;

namespace UI.Factories.Reward
{
    [UsedImplicitly]
    internal sealed class RewardViewFactory : IFactory<RewardView>
    {
        private readonly RewardView _prefab;
        private readonly Transform _container;

        internal RewardViewFactory(RewardView prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        RewardView IFactory<RewardView>.Create()
        {
            return Object.Instantiate(_prefab, _container);
        }
    }
}