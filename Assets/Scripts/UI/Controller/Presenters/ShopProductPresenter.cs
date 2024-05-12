using GameEngine.Shop;
using GameEngine.Shop.Configs;
using UI.View;
using UnityEngine;

namespace UI.Controller.Presenters
{
    internal sealed class ShopProductPresenter : IShopProductPresenter
    {
        public Sprite Icon { get; }
        public Sprite Frame { get; }
        public string Price { get; }
        public string Quantity { get; }

        private readonly IProductBuyer _productBuyer;
        private readonly IPurchaseEffect _purchaseEffect;
        private readonly ProductInfo _productInfo;
        private readonly int _price;

        internal ShopProductPresenter(ProductInfo productInfo, IProductBuyer productBuyer, IPurchaseEffect purchaseEffect)
        {
            _productBuyer = productBuyer;
            _purchaseEffect = purchaseEffect;
            _productInfo = productInfo;
            _price = productInfo.Price;

            Icon = productInfo.Icon;
            Frame = productInfo.Frame;
            Price = _price.ToString();
            Quantity = $"x{productInfo.Quantity.ToString()}";
        }
        
        void IShopProductPresenter.BuyProduct()
        {
            if (!_productBuyer.CanBuyProduct(_price)) return;
         
            _productBuyer.BuyProduct(_price);
            _purchaseEffect.Invoke(_productInfo);
        }
    }
}