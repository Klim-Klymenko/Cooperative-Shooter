using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;

namespace Objects
{
    internal sealed class SteelArms : AtomicWeapon, IInitializeGameListener, IFinishGameListener
    {
        [Get(WeaponAPI.AttackInterval)]
        protected override IAtomicValue<float> AttackInterval => _attackInterval;

        [Get(WeaponAPI.AttackRequestAction)]
        protected override IAtomicAction AttackRequestAction => _attackRequestEvent;
        
        [Get(ColliderAPI.SwitchColliderAction)]
        private readonly AtomicEvent<bool> _switchColliderEvent = new();
        
        [SerializeField]
        private Collider _collider;
        
        [SerializeField]
        private AtomicValue<float> _attackInterval;
        
        [SerializeField]
        private CollisionAttackComponent _collisionAttackComponent;
        
        private readonly AtomicEvent _attackRequestEvent = new();
        private readonly AndExpression _aliveCondition = new();
        
        private SwitchColliderMechanics _switchColliderMechanics;
        
        public override IAtomicObservable AttackRequestObservable => _attackRequestEvent;
        public override IAtomicObservable AttackObservable => _attackRequestEvent;
        
        public override void Compose()
        {
            base.Compose();
            
            _aliveCondition.Append(true.AsValue());

            _switchColliderMechanics = new SwitchColliderMechanics(_collider, _switchColliderEvent);
            
            _collisionAttackComponent.Let(it =>
            {
                it.Compose();
                it.AttackCondition.Append(_aliveCondition);
            });
            
            _collisionAttackComponent.OnEnable();
            _switchColliderMechanics.OnEnable();
        }

        void IInitializeGameListener.OnInitialize()
        {
            Compose();
        }

        private void OnTriggerEnter(Collider other)
        {
            _collisionAttackComponent.OnTriggerEnter(other);
        }

        void IFinishGameListener.OnFinish()
        {
            _aliveCondition.Append(false.AsValue());
            
            _collisionAttackComponent.OnDisable();
            _switchColliderMechanics.OnDisable();
            
            _collisionAttackComponent.Dispose();
        }
    }
}