using Atomic.Elements;

namespace GameEngine
{
    public sealed class ShootMechanics
    {
        private readonly IAtomicVariable<int> _charges;
        private readonly IAtomicAction _bulletSpawnAction;
        private readonly IAtomicObservable _shootObservable;

        public ShootMechanics(IAtomicVariable<int> charges, IAtomicAction bulletSpawnAction, IAtomicObservable shootObservable)
        {
            _charges = charges;
            _bulletSpawnAction = bulletSpawnAction;
            _shootObservable = shootObservable;
        }

        public void OnEnable()
        {
            _shootObservable.Subscribe(Shoot);
        }
        
        public void OnDisable()
        {
            _shootObservable.Unsubscribe(Shoot);
        }

        private void Shoot()
        {
            _charges.Value--;
            _bulletSpawnAction.Invoke();
        }
    }
}