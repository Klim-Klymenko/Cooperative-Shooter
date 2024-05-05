using System;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal sealed class Character_Animation
    {
        [SerializeField]
        private Animator _animator;
        
        private TakeDamageAnimationController _takeDamageAnimationController;
        private DeathAnimationController _deathAnimationController;
        private MoveAnimationController _moveAnimationController;
        private AttackAnimationController _attackAnimationController;
        
        internal void Compose(Character_Core core)
        {
            _takeDamageAnimationController = new TakeDamageAnimationController(_animator, core.TakeDamageEvent);
            _deathAnimationController = new DeathAnimationController(_animator, core.DeathObservable);
            _moveAnimationController = new MoveAnimationController(_animator, core.MoveCondition, core.CurrentWeaponIndex);
            _attackAnimationController = new AttackAnimationController(_animator, core.AttackRequestObservable, core.CurrentWeaponIndex);
        }
        
        internal void OnEnable()
        {
            _takeDamageAnimationController.OnEnable();
            _deathAnimationController.OnEnable();
            _attackAnimationController.OnEnable();
        }

        internal void Update()
        {
            _moveAnimationController.Update();
        }
        
        internal void OnDisable()
        {
            _takeDamageAnimationController.OnDisable();
            _deathAnimationController.OnDisable();
            _attackAnimationController.OnDisable();
        }
    }
}