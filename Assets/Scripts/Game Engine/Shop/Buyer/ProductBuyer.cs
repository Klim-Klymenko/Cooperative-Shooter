using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using JetBrains.Annotations;

namespace GameEngine.Shop
{
    [UsedImplicitly]
    public sealed class ProductBuyer : IProductBuyer, IStartGameListener 
    {
        private IAtomicVariableObservable<int> _currency;

        private readonly IAtomicObject _character;
        
        internal ProductBuyer(IAtomicObject character)
        {
            _character = character;
        }

        void IStartGameListener.OnStart()
        {
            _currency = _character.GetVariableObservable<int>(RewardAPI.RewardAmount);
        }

        void IProductBuyer.BuyProduct(int price)
        {
            _currency.Value -= price;
        }

        bool IProductBuyer.CanBuyProduct(int price)
        {
            return _currency.Value >= price;
        }
    }
}