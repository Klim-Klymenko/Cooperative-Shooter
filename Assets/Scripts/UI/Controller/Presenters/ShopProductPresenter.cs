using GameEngine.Shop;
using GameEngine.Shop.Configs;
using JetBrains.Annotations;
using UI.View;
using UnityEngine;

namespace UI.Controller.Presenters
{
    [UsedImplicitly]
    internal sealed class ShopProductPresenter : IShopProductPresenter
    {
        public Sprite Icon { get; }
        public Sprite Frame { get; }
        public int Price { get; }
        public int Quantity { get; }

        private readonly IProductBuyer _productBuyer;
        
        internal ShopProductPresenter(ProductInfo productInfo, IProductBuyer productBuyer)
        {
            Icon = productInfo.Icon;
            Frame = productInfo.Frame;
            Price = productInfo.Price;
            Quantity = productInfo.Quantity;
        }
        
        void IShopProductPresenter.BuyProduct()
        {
            _productBuyer.BuyProduct(Price);
        }
    }
}