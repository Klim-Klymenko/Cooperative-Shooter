using Atomic.Objects;
using GameCycle;
using JetBrains.Annotations;
using UI.Controller;
using UI.View;

namespace UI.Factories.Weapon
{
    [UsedImplicitly]
    internal sealed class CurrentWeaponAdapterFactory : ICurrentWeaponAdapterFactory
    {
        private readonly GameCycleManager _gameCycleManager;
        private readonly IAtomicObject _character;

        internal CurrentWeaponAdapterFactory(GameCycleManager gameCycleManager, IAtomicObject character)
        {
            _gameCycleManager = gameCycleManager;
            _character = character;
        }

        CurrentWeaponAdapter ICurrentWeaponAdapterFactory.Create(CurrentWeaponView weaponView)
        {
            CurrentWeaponAdapter weaponAdapter = new(weaponView, _character);
            _gameCycleManager.AddListener(weaponAdapter);

            return weaponAdapter;
        }
    }
}