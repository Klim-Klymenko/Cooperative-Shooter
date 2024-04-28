using Atomic.Elements;

namespace GameEngine
{
    public sealed class ReplenishMechanics
    {
        private readonly IAtomicObservable _replenishObservable;
        private readonly IAtomicVariable<int> _replenishable;

        public ReplenishMechanics(IAtomicObservable replenishObservable, IAtomicVariable<int> replenishable)
        {
            _replenishObservable = replenishObservable;
            _replenishable = replenishable;
        }
        
        public void OnEnable()
        {
            _replenishObservable.Subscribe(OnReplenish);
        }
        
        public void OnDisable()
        {
            _replenishObservable.Unsubscribe(OnReplenish);
        }

        private void OnReplenish()
        {
            _replenishable.Value++;
        }
    }
}