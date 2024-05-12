using Atomic.Extensions;
using Atomic.Objects;
using JetBrains.Annotations;

namespace GameEngine.Shop
{
    [UsedImplicitly]
    public sealed class GunPurchaseEffect : IPurchaseEffect
    {
        private readonly IAtomicObject _character;
        private readonly AtomicObject _gun;
        private readonly int _swapWeaponIndex;

        internal GunPurchaseEffect(IAtomicObject character, AtomicObject gun, int swapWeaponIndex)
        {
            _character = character;
            _gun = gun;
            _swapWeaponIndex = swapWeaponIndex;
        }

        void IPurchaseEffect.Invoke()
        {
            _character.InvokeAction(WeaponAPI.SwapWeaponAction, _swapWeaponIndex, _gun);
        }
    }
}