using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public sealed class PassOnTargetMechanics
    {
        private readonly IAtomicFunction<Collider, bool> _passOnCondition;
        private readonly IAtomicAction<IAtomicObject> _onCollisionAction;

        public PassOnTargetMechanics(IAtomicFunction<Collider, bool> passOnCondition, IAtomicAction<IAtomicObject> onCollisionAction)
        {
            _passOnCondition = passOnCondition;
            _onCollisionAction = onCollisionAction;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!_passOnCondition.Invoke(other)) return;
          
            IAtomicObject target = other.gameObject.GetComponent<IAtomicObject>();
            _onCollisionAction?.Invoke(target);
        }
    }
}