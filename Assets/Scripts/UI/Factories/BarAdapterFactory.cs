using Atomic.Objects;
using GameCycle;
using JetBrains.Annotations;
using UI.View;
using Zenject;

namespace UI.Factories
{
    [UsedImplicitly]
    internal sealed class BarAdapterFactory<T> : IBarAdapterFactory
        where T : IGameListener
    {
        private readonly DiContainer _diContainer;
        private readonly GameCycleManager _gameCycleManager;
        private readonly IAtomicObject _character;

        internal BarAdapterFactory(DiContainer diContainer, GameCycleManager gameCycleManager, IAtomicObject character)
        {
            _diContainer = diContainer;
            _gameCycleManager = gameCycleManager;
            _character = character;
        }

        void IBarAdapterFactory.Create(BarView barView)
        {
            object[] extraArgs = { barView, _character };
            T barAdapter = _diContainer.Instantiate<T>(extraArgs);
            
            _gameCycleManager.AddListener(barAdapter);
        }
    }
}