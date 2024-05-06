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
        private IAtomicVariableObservable<int> _hitPoints;
        
        private readonly BarView _healthView;
        private readonly IAtomicObject _character;

        internal HealthAdapter(BarView healthView, IAtomicObject character)
        {
            _healthView = healthView;
            _character = character;
        }

        void IStartGameListener.OnStart()
        {
            _hitPoints = _character.GetVariableObservable<int>(LiveableAPI.HitPoints);
            _hitPoints.Subscribe(ChangeHealth);
            ChangeHealth(_hitPoints.Value);
        }

        void IFinishGameListener.OnFinish()
        {
            _hitPoints.Unsubscribe(ChangeHealth);
        }

        private void ChangeHealth(int health)
        {
            _healthView.ChangeValue(health);
        }
    }
}