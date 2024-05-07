using Atomic.Objects;
using Common;
using Objects.Reward;
using UnityEngine;
using Zenject;

namespace Objects
{
    internal sealed class RewardInstaller : MonoInstaller
    {
        [SerializeField] 
        private int _poolSize;
        
        [SerializeField]
        private Reward.Reward _prefab;
        
        [SerializeField]
        private Transform _poolContainer;
        
        public override void InstallBindings()
        {
            BindPool();
            BindSpawner();
        }

        private void BindPool()
        {
            Container.Bind<Common.Pool<Reward.Reward>>().AsSingle().WithArguments(_poolSize, _prefab, _poolContainer);
        }

        private void BindSpawner()
        {
            Container.Bind<ISpawner<AtomicObject, Transform>>().To<RewardSpawner>().AsSingle();
        }
    }
}