using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.View.Popup
{
    internal sealed class ShopProductView : MonoBehaviour
    {
        [SerializeField]
        private Image _iconImage;
        
        [SerializeField]
        private Image _frameImage;
        
        [SerializeField]
        private TextMeshProUGUI _priceText;
        
        [SerializeField]
        private TextMeshProUGUI _quantityText;
        
        internal void Show(IShopProductPresenter productPresenter)
        {
            
        }

        internal void Hide()
        {
            
        }
    }
}