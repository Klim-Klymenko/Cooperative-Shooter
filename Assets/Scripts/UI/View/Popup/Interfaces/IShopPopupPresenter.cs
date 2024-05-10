using System.Collections.Generic;

namespace UI.View
{
    public interface IShopPopupPresenter
    {
        IReadOnlyList<IShopProductPresenter> ProductPresenters { get; }
    }
}