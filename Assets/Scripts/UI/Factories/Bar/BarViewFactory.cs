using Common;
using JetBrains.Annotations;
using UI.View;
using UnityEngine;

namespace UI.Factories
{
    [UsedImplicitly]
    internal sealed class BarViewFactory : IFactory<BarView>
    {
        private readonly BarView _viewPrefab;
        private readonly Transform _container;
        
        internal BarViewFactory(BarView viewPrefab, Transform container)
        {
            _viewPrefab = viewPrefab;
            _container = container;
        }

        BarView IFactory<BarView>.Create()
        {
            return Object.Instantiate(_viewPrefab, _container);
        }
    }
}