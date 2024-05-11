using JetBrains.Annotations;
using UI.Controller.Presenters;
using Zenject;

namespace UI.Factories.Shop.Presenter
{
    [UsedImplicitly]
    internal sealed class ShopPopupPresenterFactory : Common.IFactory<ShopPopupPresenter>
    {
        private readonly DiContainer _diContainer;

        internal ShopPopupPresenterFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        ShopPopupPresenter Common.IFactory<ShopPopupPresenter>.Create()
        {
            return _diContainer.Instantiate<ShopPopupPresenter>();
        }
    }
}