using Common;
using JetBrains.Annotations;
using UI.View.Popup;
using UnityEngine;

namespace UI.Factories.Shop
{
    [UsedImplicitly]
    internal sealed class ShopProductViewSpawner : IShopProductViewSpawner
    {
        private int _currentIndex;
        
        private readonly Pool<ShopProductView>[] _viewPools;
        private readonly Transform[] _prefabsTransforms;

        internal ShopProductViewSpawner(Pool<ShopProductView>[] viewPools, Transform[] prefabsTransforms)
        {
            _viewPools = viewPools;
            _prefabsTransforms = prefabsTransforms;
        }

        ShopProductView IShopProductViewSpawner.Spawn(Transform container)
        {
            ShopProductView view = _viewPools[_currentIndex].Get();
            
            Transform viewTransform = view.transform;
            
            viewTransform.SetParent(container);
            viewTransform.localPosition = _prefabsTransforms[_currentIndex].position;
            viewTransform.localScale = _prefabsTransforms[_currentIndex].localScale;
            
            if (_currentIndex < _viewPools.Length - 1)
                _currentIndex++;
            else
                _currentIndex = 0;
            
            return view;
        }

        void IShopProductViewSpawner.Despawn(ShopProductView view)
        {
            for (int i = 0; i < _viewPools.Length; i++)
            {
                Pool<ShopProductView> pool = _viewPools[i];

                if (pool.ObjectType != view.Id) continue;
                
                pool.Put(view);
                break;
            }
        }
    }
}