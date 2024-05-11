using Atomic.Objects;
using UI.Factories.Reward;
using UI.Managers;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI.DI
{
    internal sealed class RewardUIInstaller : MonoInstaller
    {
        [SerializeField]
        private RewardView _prefab;
        
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
            Container.Bind<Common.IFactory<RewardView>>().To<RewardViewFactory>().AsSingle().WithArguments(_prefab, _container);
            Container.Bind<IRewardAdapterFactory>().To<RewardAdapterFactory>().AsSingle().WithArguments(_character);
        }

        private void BindManager()
        {
            Container.BindInterfacesTo<RewardManager>().AsCached();
        }
    }
}