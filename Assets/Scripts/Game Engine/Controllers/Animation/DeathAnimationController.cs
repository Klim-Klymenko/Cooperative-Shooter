using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class DeathAnimationController
    {
        private static readonly int _deathTrigger = Animator.StringToHash("Death");

        private readonly Animator _animator;
        private readonly IAtomicObservable _deathObservable;

        public DeathAnimationController(Animator animator, IAtomicObservable deathObservable)
        {
            _animator = animator;
            _deathObservable = deathObservable;
        }
        
        public void OnEnable()
        {
            _deathObservable.Subscribe(OnDeath);
        }
        
        public void OnDisable()
        {
            _deathObservable.Unsubscribe(OnDeath);
        }

        private void OnDeath()
        {
            _animator.SetTrigger(_deathTrigger);
        }
    }
}