using Common;
using JetBrains.Annotations;
using UI.Controller.Presenters;
using UI.View.Popup;

namespace UI.Managers
{
    [UsedImplicitly]
    internal sealed class ShopPopupManager : IPopupManager
    {
        private ShopPopupView _view;
        private ShopPopupPresenter _presenter;
        
        private readonly ISpawner<ShopPopupView> _viewSpawner;
        private readonly IFactory<ShopPopupPresenter> _presenterFactory;

        internal ShopPopupManager(ISpawner<ShopPopupView> viewSpawner, IFactory<ShopPopupPresenter> presenterFactory)
        {
            _viewSpawner = viewSpawner;
            _presenterFactory = presenterFactory;
        }

        void IPopupManager.Show()
        {
            _presenter = _presenterFactory.Create();
            _view = _viewSpawner.Spawn();
            
            _view.Show(_presenter);
        }

        void IPopupManager.Hide()
        {
            _view.Hide();
            _viewSpawner.Despawn(_view);
            
            _view = null;
            _presenter = null;
        }
    }
}