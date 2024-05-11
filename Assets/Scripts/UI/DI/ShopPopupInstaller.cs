using System.Linq;
using Atomic.Objects;
using Common;
using GameEngine.Shop;
using GameEngine.Shop.Configs;
using UI.Controller.Presenters;
using UI.Factories.Shop;
using UI.Factories.Shop.Presenter;
using UI.Managers;
using UI.View.Popup;
using UnityEngine;
using Zenject;

namespace UI.DI
{
    internal sealed class ShopPopupInstaller : MonoInstaller
    {
        [SerializeField]
        private ProductCatalog _productCatalog;

        [SerializeField] 
        private AtomicObject _character;
        
        [SerializeField]
        private Transform _popupViewUIContainer;
        
        [Header("Popup")]
        
        [SerializeField]
        private int _popupPoolSize;
        
        [SerializeField]
        private ShopPopupView _popupViewPrefab;
        
        [SerializeField]
        private Transform _popupContainer;
        
        [Header("Product")]
        
        [SerializeField]
        private int _productPoolSize;
        
        [SerializeField]
        private ShopProductView[] _productViewPrefabs;
        
        [SerializeField]
        private Transform _productContainer;
        
        public override void InstallBindings()
        {
            BindBuyer();
            BindManager();
            BindCatalog();
            BindPools();
            BindFactories();
        }

        private void BindBuyer()
        {
            Container.BindInterfacesTo<ProductBuyer>().AsSingle().WithArguments(_character);
        }

        private void BindManager()
        {
            Container.Bind<IPopupManager>().To<ShopPopupManager>().AsSingle();
        }

        private void BindCatalog()
        {
            Container.Bind<ProductCatalog>().FromInstance(_productCatalog).AsSingle();
        }
        
        private void BindPools()
        {
            Container.Bind<Pool<ShopPopupView>>().AsSingle().WithArguments(_popupPoolSize, _popupViewPrefab, _popupContainer);

            Pool<ShopProductView>[] productPools = new Pool<ShopProductView>[_productViewPrefabs.Length];

            for (int i = 0; i < productPools.Length; i++)
            {
                ShopProductView prefab = _productViewPrefabs[i];
                productPools[i] = new Pool<ShopProductView>(Container, _productPoolSize, prefab, _productContainer, prefab.Id);
            }
            
            Container.Bind<Pool<ShopProductView>[]>().FromInstance(productPools).AsSingle();
        }
        
        private void BindFactories()
        {
            BindView();
            BindPresenter();
            
            return;

            void BindView()
            {
                Container.Bind<ISpawner<ShopPopupView>>().To<ShopPopupViewSpawner>().AsSingle()
                    .WithArguments(_popupViewUIContainer, _popupViewPrefab.transform);

                Container.Bind<IShopProductViewSpawner>().To<ShopProductViewSpawner>().AsSingle()
                    .WithArguments(_productViewPrefabs.Select(prefab => prefab.transform).ToArray());
            }
            
            void BindPresenter()
            {
                Container.Bind<Common.IFactory<ShopPopupPresenter>>().To<ShopPopupPresenterFactory>().AsSingle();
            }
        }
    }
}