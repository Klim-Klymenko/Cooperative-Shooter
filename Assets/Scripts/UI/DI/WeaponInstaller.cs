using Atomic.Objects;
using UI.Factories.Weapon;
using UI.Managers;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI.DI
{
    internal sealed class WeaponInstaller : MonoInstaller
    {
        [SerializeField]
        private CurrentWeaponView _viewPrefab;
        
        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private AtomicObject _character;
        
        public override void InstallBindings()
        {
            BindFactories();
            BindManager();
        }
        
        private void BindFactories()
        {
            Container.Bind<Common.IFactory<CurrentWeaponView>>().To<CurrentWeaponViewFactory>().AsSingle().WithArguments(_viewPrefab, _container);
            Container.Bind<ICurrentWeaponAdapterFactory>().To<CurrentWeaponAdapterFactory>().AsSingle().WithArguments(_character);
        }

        private void BindManager()
        {
            Container.BindInterfacesTo<WeaponManager>().AsCached();
        }
    }
}