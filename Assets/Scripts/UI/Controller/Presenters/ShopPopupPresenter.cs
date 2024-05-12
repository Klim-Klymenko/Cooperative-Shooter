using System.Collections.Generic;
using GameEngine.Shop;
using GameEngine.Shop.Configs;
using JetBrains.Annotations;
using UI.View;

namespace UI.Controller.Presenters
{
    [UsedImplicitly]
    internal sealed class ShopPopupPresenter : IShopPopupPresenter
    {
        private readonly IShopProductPresenter[] _productPresenters;
        public IReadOnlyList<IShopProductPresenter> ProductPresenters => _productPresenters;
        
        internal ShopPopupPresenter(ProductCatalog catalog, IProductBuyer productBuyer, IPurchaseEffect[] purchaseEffects)
        {
            IReadOnlyList<ProductInfo> productInfos = catalog.ProductInfos;
            
            _productPresenters = new IShopProductPresenter[productInfos.Count];
           
            for (int i = 0; i < _productPresenters.Length; i++)
            {
                ShopProductPresenter productPresenter = new(productInfos[i], productBuyer, purchaseEffects[i]);
                _productPresenters[i] = productPresenter;
            }
        }
    }
}