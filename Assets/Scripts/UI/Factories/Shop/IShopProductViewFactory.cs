using UI.View;
using UI.View.Popup;

namespace UI.Factories.Shop
{
    internal interface IShopProductViewFactory
    {
        ShopProductView Create(IShopProductPresenter productPresenter);
    }
}