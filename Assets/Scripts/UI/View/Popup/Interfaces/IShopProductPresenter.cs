using UnityEngine;

namespace UI.View
{
    public interface IShopProductPresenter
    {
        Sprite Icon { get; }
        Sprite Frame { get; }
        string Price { get; }
        string Quantity { get; }
        
        void BuyProduct();
    }
}