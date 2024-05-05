using Atomic.Elements;
using Atomic.Extensions;

namespace GameEngine
{
    public static class ColliderAPI
    {
        [Contract(typeof(IAtomicAction<bool>))]
        public const string SwitchColliderAction = nameof(SwitchColliderAction);
    }
}