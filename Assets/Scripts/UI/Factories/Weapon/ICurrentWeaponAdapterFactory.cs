using UI.Controller;
using UI.View;

namespace UI.Factories.Weapon
{
    internal interface ICurrentWeaponAdapterFactory
    {
        CurrentWeaponAdapter Create(CurrentWeaponView weaponView);
    }
}