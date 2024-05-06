using Atomic.Elements;
using Atomic.Extensions;

namespace GameEngine
{
    public static class LiveableAPI
    {
        [Contract(typeof(IAtomicObservable<int>))]
        public const string HitPointsObservable = nameof(HitPointsObservable);
        
        [Contract(typeof(IAtomicAction<int>))]
        public const string TakeDamageAction = nameof(TakeDamageAction);
        
        [Contract(typeof(IAtomicObservable))]
        public const string DeathObservable = nameof(DeathObservable);
    }
}