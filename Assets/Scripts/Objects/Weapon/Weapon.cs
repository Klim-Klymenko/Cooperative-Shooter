using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common;
using GameCycle;
using GameEngine;
using GameEngine.Data;
using UnityEngine;
using Zenject;

namespace Objects
{
    public sealed class Weapon : AtomicObject, IInitializeGameListener, IUpdateGameListener, IFinishGameListener
    {
        [Get(WeaponAPI.AttackInterval)]
        private IAtomicValue<float> ShootingInterval => _shootComponent.ShootingInterval;
        
        [Get(WeaponAPI.AttackRequestAction)]
        private IAtomicAction ShootRequestAction => _shootComponent.ShootRequestAction;
        
        [Get(WeaponAPI.AttackAction)]
        private IAtomicAction ShootAction => _shootComponent.ShootAction;
        
        [Get(SoundAPI.AttackClip)]
        [SerializeField]
        private AudioClip _attackClip;

        [Get(ParticleAPI.AttackParticle)] 
        [SerializeField]
        private ParticleSystem _attackParticle;
        
        [SerializeField]
        private WeaponType _weaponType;
        
        [SerializeField] 
        private ShootComponent _shootComponent;
        
        [SerializeField]
        private ReplenishComponent _replenishComponent;
        
        private readonly AndExpression _aliveCondition = new();
        
        internal IAtomicObservable ShootRequestObservable => _shootComponent.ShootRequestObservable;
        internal IAtomicObservable ShootObservable => _shootComponent.ShootObservable;
        
        private ISpawner<Transform> _bulletSpawner;
        
        [Inject]
        internal void Construct(ISpawner<Transform> bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }
        
        public override void Compose()
        {
            base.Compose();
            
            _aliveCondition.Append(true.AsValue());
            
            _shootComponent.Let(it =>
            {
                it.Compose(_bulletSpawner);
                it.ShootCondition.Append(_aliveCondition);
            });

            _replenishComponent.Let(it =>
            {
                it.Compose(_shootComponent.Charges);
                it.ReplenishCondition.Append(_aliveCondition);
            });
            
            _replenishComponent.OnEnable();
            _shootComponent.OnEnable();
            
            AddType(_weaponType.ToString());
        }

        void IInitializeGameListener.OnInitialize()
        {
            Compose();
        }

        void IUpdateGameListener.OnUpdate()
        {
            _replenishComponent.Update();
        }

        void IFinishGameListener.OnFinish()
        {
            _aliveCondition.Append(false.AsValue());
            
            _replenishComponent.OnDisable();
            _shootComponent.OnDisable();
            
            _shootComponent.Dispose();
            _replenishComponent.Dispose();
        }
    }
}