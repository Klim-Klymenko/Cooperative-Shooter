using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine.Shop.Configs;
using JetBrains.Annotations;

namespace GameEngine.Shop
{
    [UsedImplicitly]
    public sealed class BulletsPurchaseEffect : IPurchaseEffect
    {
        private readonly IAtomicObject _character;
        private readonly int _gunIndex;

        public BulletsPurchaseEffect(IAtomicObject character, int gunIndex)
        {
            _character = character;
            _gunIndex = gunIndex;
        }

        void IPurchaseEffect.Invoke(ProductInfo productInfo)
        {
            AtomicObject[] weapons = _character.Get<AtomicObject[]>(WeaponAPI.Weapons);
            AtomicObject weapon = weapons[_gunIndex];

            IAtomicVariable<int> charges = weapon.GetVariableObservable<int>(WeaponAPI.Charges);
            charges.Value += productInfo.Quantity;
        }
    }
}