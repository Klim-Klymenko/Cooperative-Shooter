using System.Collections.Generic;
using UI.Factories.Shop;
using UnityEngine;
using Zenject;

namespace UI.View.Popup
{
    public sealed class ShopPopupView : MonoBehaviour
    {
        private IShopProductViewFactory _productViewFactory;

        private ShopProductView[] _productViews;
        
        [Inject]
        internal void Construct(IShopProductViewFactory productViewFactory)
        {
            _productViewFactory = productViewFactory;
        }
        
        public void Show(IShopPopupPresenter popupPresenter)
        {
            IReadOnlyList<IShopProductPresenter> productPresenters = popupPresenter.ProductPresenters;
            
            _productViews = new ShopProductView[productPresenters.Count];
            
            for (int i = 0; i < productPresenters.Count; i++)
            {
                ShopProductView productView = _productViewFactory.Create(productPresenters[i]);
                _productViews[i] = productView;
            }
        }

        public void Hide()
        {
            for (int i = 0; i < _productViews.Length; i++)
                _productViews[i].Hide();
        }
    }
}