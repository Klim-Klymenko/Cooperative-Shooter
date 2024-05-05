using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;

namespace Objects
{
    public sealed class SteelArmsAnimatorDispatcher : MonoBehaviour, IStartGameListener
    {
        [SerializeField]
        private AtomicObject _steelArms;

        private IAtomicAction<bool> _switchColliderAction;
      
        void IStartGameListener.OnStart()
        {
            _switchColliderAction = _steelArms.GetAction<bool>(ColliderAPI.SwitchColliderAction);
        }
        
        public void OnSwitchOnCollider()
        {
            _switchColliderAction.Invoke(true);
        }
        
        public void OnSwitchOffCollider()
        {
            _switchColliderAction.Invoke(false);
        }
    }
}