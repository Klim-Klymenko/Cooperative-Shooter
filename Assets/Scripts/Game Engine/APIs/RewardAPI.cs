using Atomic.Elements;
using Atomic.Extensions;

namespace GameEngine
{
    public static class RewardAPI
    {
        [Contract(typeof(IAtomicVariable<int>))] 
        public const string Reward = nameof(Reward);
        
        [Contract(typeof(IAtomicValueObservable<int>))]
        public const string RewardAmount = nameof(RewardAmount);
    }
}