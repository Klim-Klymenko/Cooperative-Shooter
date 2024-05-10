using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;

namespace GameEngine.Shop
{
    [UsedImplicitly]
    internal sealed class ProductBuyer : IProductBuyer, IStartGameListener 
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
            if (_currency.Value >= price)
                _currency.Value -= price;
        }
    }
}