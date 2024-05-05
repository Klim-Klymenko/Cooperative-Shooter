using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public sealed class PassOnTargetMechanics
    {
        private readonly IAtomicFunction<Collider, bool> _passOnCondition;
        private readonly IAtomicAction<AtomicObject> _onCollisionAction;

        public PassOnTargetMechanics(IAtomicFunction<Collider, bool> passOnCondition, IAtomicAction<AtomicObject> onCollisionAction)
        {
            _passOnCondition = passOnCondition;
            _onCollisionAction = onCollisionAction;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!_passOnCondition.Invoke(other)) return;
            
            AtomicObject target = other.gameObject.GetComponent<AtomicObject>();
            _onCollisionAction?.Invoke(target);
        }
    }
}