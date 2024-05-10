using UnityEngine;

namespace UI.View
{
    public interface IShopProductPresenter
    {
        Sprite Icon { get; }
        Sprite Frame { get; }
        int Price { get; }
        int Quantity { get; }
        
        void BuyProduct();
    }
}