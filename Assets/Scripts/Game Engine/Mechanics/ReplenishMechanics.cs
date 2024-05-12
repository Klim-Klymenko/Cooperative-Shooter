using Atomic.Elements;

namespace GameEngine
{
    public sealed class ReplenishMechanics
    {
        private readonly IAtomicObservable _replenishObservable;
        private readonly IAtomicVariable<int> _replenishable;
        private readonly IAtomicValue<bool> _replenishCondition;

        public ReplenishMechanics(IAtomicObservable replenishObservable, IAtomicVariable<int> replenishable, IAtomicValue<bool> replenishCondition)
        {
            _replenishObservable = replenishObservable;
            _replenishable = replenishable;
            _replenishCondition = replenishCondition;
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
            if (_replenishCondition.Value)
                _replenishable.Value++;
        }
    }
}