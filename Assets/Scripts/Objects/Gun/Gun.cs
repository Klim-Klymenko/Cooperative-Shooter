using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common;
using GameCycle;
using GameEngine;
using UnityEngine;
using Zenject;

namespace Objects
{
    [Is(ObjectTypes.Striker)]
    internal sealed class Gun : AtomicObject, IInitializeGameListener, IUpdateGameListener, IFinishGameListener
    {
        [Get(SwitchableAPI.SwitchOnAction)]
        private IAtomicAction SwitchOnAction => _switchGameObjectComponent.SwitchOnAction;
        
        [Get(SwitchableAPI.SwitchOffAction)]
        private IAtomicAction SwitchOffAction => _switchGameObjectComponent.SwitchOffAction;
        
        [Get(ShooterAPI.ShootingInterval)]
        private IAtomicValue<float> ShootingInterval => _shootComponent.ShootingInterval;
        
        [Get(ShooterAPI.ShootAction)]
        private IAtomicAction ShootAction => _shootComponent.ShootAction;
        
        [SerializeField]
        private SwitchGameObjectComponent _switchGameObjectComponent;
        
        [SerializeField] 
        private ShootComponent _shootComponent;
        
        [SerializeField]
        private ReplenishComponent _replenishComponent;
        
        private readonly AndExpression _aliveCondition = new();
        
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
            
            ISpawner<Transform> bulletSpawner = _bulletSpawner;
            _aliveCondition.Append(true.AsValue());
            
            _switchGameObjectComponent.Compose(gameObject);

            _shootComponent.Let(it =>
            {
                it.Compose(bulletSpawner);
                it.ShootCondition.Append(_aliveCondition);
            });

            _replenishComponent.Let(it =>
            {
                it.Compose(_shootComponent.Charges);
                it.ReplenishCondition.Append(_aliveCondition);
            });
            
            _switchGameObjectComponent.OnEnable();
            _replenishComponent.OnEnable();
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
            
            _switchGameObjectComponent.OnDisable();
            _replenishComponent.OnDisable();
            
            _switchGameObjectComponent.Dispose();
            _shootComponent.Dispose();
            _replenishComponent.Dispose();
        }
    }
}