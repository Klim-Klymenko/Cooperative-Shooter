using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using GameEngine.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Objects
{
    [Is(TypeAPI.Movable)]
    internal sealed class Bullet : AtomicObject, IDisposable, IUpdateGameListener, IFinishGameListener
    {
        [Get(MovableAPI.MovementDirection)]
        private IAtomicVariable<Vector3> MoveDirection => _moveComponent.MovementDirection;

        [Get(MovableAPI.MoveCondition)]
        private IAtomicValue<bool> MoveCondition => _moveComponent.MoveCondition;
        
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private BulletType _bulletType;
        
        [SerializeField]
        private MoveComponent _moveComponent;
        
        [SerializeField]
        private CollisionAttackComponent _collisionAttackComponent;
        
        [Get(LiveableAPI.DeathObservable)]
        private readonly AtomicEvent _deathEvent = new();
        
        private readonly AtomicVariable<bool> _aliveCondition = new();
        
        private bool _composed;
        
        public override void Compose()
        {
            base.Compose();
            
           AtomicVariable<Vector3> moveDirection = new(_transform.forward);

           _moveComponent.Let(it =>
           {
               it.Compose(_transform, moveDirection);
               it.MoveCondition.Append(_aliveCondition);
           });

           _collisionAttackComponent.Let(it =>
           {
               it.Compose();
               it.AttackCondition.Append(_aliveCondition);
           });
           
           _collisionAttackComponent.OnEnable();

            _aliveCondition.Value = true;
            _composed = true;

            AddType(_bulletType.ToString());
        }

        void IUpdateGameListener.OnUpdate()
        {
            if (!_composed) return;
           
            _moveComponent.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_composed) return;
            
           _collisionAttackComponent.OnTriggerEnter(other);
           
           _deathEvent?.Invoke();
           _aliveCondition.Value = false;
        }

        public void OnFinish()
        {
            if (!_composed) return;
            
            _collisionAttackComponent.OnDisable();
            
            Dispose();
        }

        public void Dispose()
        {
            _deathEvent?.Dispose();
            _moveComponent?.Dispose();
            _collisionAttackComponent.Dispose();
        }
    }
}