using GameEngine.Shop;
using GameEngine.Shop.Configs;
using JetBrains.Annotations;
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
        private readonly int _price;
        
        internal ShopProductPresenter(ProductInfo productInfo, IProductBuyer productBuyer)
        {
            _productBuyer = productBuyer;
            _price = productInfo.Price;
            
            Icon = productInfo.Icon;
            Frame = productInfo.Frame;
            Price = _price.ToString();
            Quantity = $"x{productInfo.Quantity.ToString()}";
        }
        
        void IShopProductPresenter.BuyProduct()
        {
            _productBuyer.BuyProduct(_price);
        }
    }
}