using UI.View.Popup;
using UnityEngine;

namespace UI.Factories.Shop
{
    internal interface IShopProductViewSpawner
    {
        ShopProductView Spawn(Transform container);
        void Despawn(ShopProductView view);
    }
}