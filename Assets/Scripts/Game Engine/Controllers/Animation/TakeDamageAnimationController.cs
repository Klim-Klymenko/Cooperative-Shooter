using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class TakeDamageAnimationController
    {
        private static readonly int _takeDamageTrigger = Animator.StringToHash("Take Damage");

        private readonly Animator _animator;
        private readonly IAtomicObservable<int> _takeDamageObservable;

        public TakeDamageAnimationController(Animator animator, IAtomicObservable<int> takeDamageObservable)
        {
            _animator = animator;
            _takeDamageObservable = takeDamageObservable;
        }

        public void OnEnable()
        {
            _takeDamageObservable.Subscribe(OnTakeDamage);
        }
        
        public void OnDisable()
        {
            _takeDamageObservable.Unsubscribe(OnTakeDamage);
        }
        
        private void OnTakeDamage(int _)
        {
            _animator.SetTrigger(_takeDamageTrigger);
        }
    }
}