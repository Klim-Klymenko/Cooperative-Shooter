using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        
        [SerializeField]
        private Button _buyButton;

        private IShopProductPresenter _productPresenter;

        internal string Id => _iconImage.sprite.name;
        
        internal void Show(IShopProductPresenter productPresenter)
        {
            _productPresenter = productPresenter;
            
            UpdateIcon(productPresenter.Icon);
            UpdateFrame(productPresenter.Frame);
            UpdatePrice(productPresenter.Price);
            UpdateQuantity(productPresenter.Quantity);
            
            _buyButton.onClick.AddListener(productPresenter.BuyProduct);
        }

        internal void Hide()
        {
            _buyButton.onClick.RemoveListener(_productPresenter.BuyProduct);
        }
        
        private void UpdateIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }
        
        private void UpdateFrame(Sprite frame)
        {
            _frameImage.sprite = frame;
        }
        
        private void UpdatePrice(string price)
        {
            _priceText.text = price;
        }
        
        private void UpdateQuantity(string quantity)
        {
            _quantityText.text = quantity;
        }
    }
}