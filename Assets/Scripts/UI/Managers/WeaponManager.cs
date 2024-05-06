using Common;
using GameCycle;
using JetBrains.Annotations;
using UI.Factories.Weapon;
using UI.View;

namespace UI.Managers
{
    [UsedImplicitly]
    internal sealed class WeaponManager : IStartGameListener
    {
        private readonly IFactory<CurrentWeaponView> _weaponViewFactory;
        private readonly CurrentWeaponAdapterFactory _weaponAdapterFactory;

        internal WeaponManager(IFactory<CurrentWeaponView> weaponViewFactory, CurrentWeaponAdapterFactory weaponAdapterFactory)
        {
            _weaponViewFactory = weaponViewFactory;
            _weaponAdapterFactory = weaponAdapterFactory;
        }

        void IStartGameListener.OnStart()
        {
            CurrentWeaponView weaponView = _weaponViewFactory.Create();
            _weaponAdapterFactory.Create(weaponView);
        }
    }
}