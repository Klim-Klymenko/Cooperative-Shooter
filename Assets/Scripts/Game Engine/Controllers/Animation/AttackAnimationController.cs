using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class AttackAnimationController
    {
        private static readonly int _attackTrigger = Animator.StringToHash("Attack");
        
        private readonly Animator _animator;
        private readonly IAtomicObservable _attackObservable;

        public AttackAnimationController(Animator animator, IAtomicObservable attackObservable)
        {
            _animator = animator;
            _attackObservable = attackObservable;
        }
        
        public void OnEnable()
        {
            _attackObservable.Subscribe(OnShoot);
        }
        
        public void OnDisable()
        {
            _attackObservable.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            _animator.SetTrigger(_attackTrigger);
        }
    }
}