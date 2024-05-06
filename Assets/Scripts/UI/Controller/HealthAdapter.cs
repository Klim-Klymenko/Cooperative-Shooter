using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UI.View;

namespace UI.Controller
{
    [UsedImplicitly]
    internal sealed class HealthAdapter : IStartGameListener, IFinishGameListener
    {
        private IAtomicObservable<int> _hitPointsObservable;
        
        private readonly BarView _healthView;
        private readonly IAtomicObject _character;

        internal HealthAdapter(BarView healthView, IAtomicObject character)
        {
            _healthView = healthView;
            _character = character;
        }

        void IStartGameListener.OnStart()
        {
            _hitPointsObservable = _character.GetObservable<int>(LiveableAPI.HitPointsObservable);
            _hitPointsObservable.Subscribe(ChangeHealth);
        }

        void IFinishGameListener.OnFinish()
        {
            _hitPointsObservable.Unsubscribe(ChangeHealth);
        }

        private void ChangeHealth(int health)
        {
            _healthView.ChangeValue(health);
        }
    }
}