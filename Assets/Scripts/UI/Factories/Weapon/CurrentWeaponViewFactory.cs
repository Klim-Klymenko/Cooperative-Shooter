using Common;
using JetBrains.Annotations;
using UI.View;
using UnityEngine;

namespace UI.Factories.Weapon
{
    [UsedImplicitly]
    internal sealed class CurrentWeaponViewFactory : IFactory<CurrentWeaponView>
    {
        private readonly CurrentWeaponView _viewPrefab;
        private readonly Transform _container;

        internal CurrentWeaponViewFactory(CurrentWeaponView viewPrefab, Transform container)
        {
            _viewPrefab = viewPrefab;
            _container = container;
        }

        CurrentWeaponView IFactory<CurrentWeaponView>.Create()
        {
            return Object.Instantiate(_viewPrefab, _container);
        }
    }
}