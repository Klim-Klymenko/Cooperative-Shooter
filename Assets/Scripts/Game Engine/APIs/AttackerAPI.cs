using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public static class AttackerAPI
    {
        [Contract(typeof(IAtomicVariable<Transform>))]
        public const string TargetTransform = nameof(TargetTransform);
        
        [Contract(typeof(IAtomicAction<AtomicObject>))]
        public const string AttackAction = nameof(AttackAction);
        
        [Contract(typeof(IAtomicObservable))]
        public const string AttackObservable = nameof(AttackObservable);
    }
}