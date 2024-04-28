using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Serializable]
    internal class Zombie_AI
    {
        [SerializeField]
        private NavMeshAgentComponent _agentComponent;
        
        internal IAtomicValue<bool> MoveCondition => _agentComponent.MoveCondition;
        
        internal void Compose(Zombie_Core core)
        {
            _agentComponent.Let(it =>
            {
                it.Compose(core.TargetTransform);
                it.MoveCondition.Append(core.AliveCondition);
            });
        }

        public void OnEnable()
        {
            _agentComponent.OnEnable();
        }
        
        public void Update()
        {
            _agentComponent.Update();
        }
        
        public void OnDisable()
        {
            _agentComponent.OnDisable();
        }
    }
}