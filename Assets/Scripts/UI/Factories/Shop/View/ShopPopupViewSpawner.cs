using Common;
using JetBrains.Annotations;
using UI.View.Popup;
using UnityEngine;

namespace UI.Factories.Shop
{
    [UsedImplicitly]
    internal sealed class ShopPopupViewSpawner : ISpawner<ShopPopupView>
    {
        private readonly Pool<ShopPopupView> _viewPool;
        private readonly Transform _container;
        private readonly Transform _prefabTransform;
        
        internal ShopPopupViewSpawner(Pool<ShopPopupView> viewPool, Transform container, Transform prefabTransform)
        {
            _viewPool = viewPool;
            _container = container;
            _prefabTransform = prefabTransform;
        }
        
        ShopPopupView ISpawner<ShopPopupView>.Spawn()
        {
            ShopPopupView view = _viewPool.Get();
            
            Transform viewTransform = view.transform;
            
            viewTransform.SetParent(_container);
            viewTransform.localPosition = _prefabTransform.position;
            viewTransform.localScale = _prefabTransform.localScale;

            return view;
        }

        void ISpawner<ShopPopupView>.Despawn(ShopPopupView view)
        {
            _viewPool.Put(view);
        }
    }
}