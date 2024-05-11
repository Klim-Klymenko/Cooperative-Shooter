using System.Collections.Generic;
using UI.Factories.Shop;
using UnityEngine;
using Zenject;

namespace UI.View.Popup
{
    internal sealed class ShopPopupView : MonoBehaviour
    {
        [SerializeField]
        private Transform _itemContainer;
        
        private IShopProductViewSpawner _productViewSpawner;

        private ShopProductView[] _productViews;
        
        [Inject]
        internal void Construct(IShopProductViewSpawner productViewSpawner)
        {
            _productViewSpawner = productViewSpawner;
        }
        
        internal void Show(IShopPopupPresenter popupPresenter)
        {
            IReadOnlyList<IShopProductPresenter> productPresenters = popupPresenter.ProductPresenters;
            
            _productViews = new ShopProductView[productPresenters.Count];
            
            for (int i = 0; i < productPresenters.Count; i++)
            {
                ShopProductView productView = _productViewSpawner.Spawn(_itemContainer);
                
                productView.Show(productPresenters[i]);
                _productViews[i] = productView;
            }
        }

        internal void Hide()
        {
            for (int i = 0; i < _productViews.Length; i++)
            {
                ShopProductView view = _productViews[i];
                
                view.Hide();
                _productViewSpawner.Despawn(view);
            }

            _productViews = null;
        }
    }
}