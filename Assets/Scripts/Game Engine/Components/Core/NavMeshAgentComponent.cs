using System;
using Atomic.Elements;
using UnityEngine;
using UnityEngine.AI;

namespace GameEngine
{
    [Serializable]
    public sealed class NavMeshAgentComponent
    {
        [SerializeField] 
        private NavMeshAgent _agent;
        
        private float _stoppingDistance;
        
        private IAtomicValue<Transform> _targetTransform;
        
        private readonly AndExpression _followCondition = new();
        private readonly AndExpression _moveCondition = new();

        [SerializeField]
        [HideInInspector]
        private AtomicFunction<Vector3> _targetPosition;
        
        private SwitchNavMeshAgentMechanics _switchNavMeshAgentMechanics;
        private AgentFollowMechanics _agentFollowMechanics;
        
        public IAtomicExpression<bool> MoveCondition => _moveCondition;
        
        public void Compose(IAtomicValue<Transform> targetTransform) 
        {
            _targetTransform = targetTransform;
            _stoppingDistance = _agent.stoppingDistance;
                
            _followCondition.Append(new AtomicFunction<bool>(() => _targetTransform.Value != null));
            _followCondition.Append(new AtomicFunction<bool>(() => _agent.isOnNavMesh));
            
            _moveCondition.Append(_followCondition);
            _moveCondition.Append(new AtomicFunction<bool>(() => _agent.remainingDistance > _stoppingDistance));
            
            _targetPosition.Compose(() => _targetTransform.Value == null ? Vector3.zero : _targetTransform.Value.position);
            
            _switchNavMeshAgentMechanics = new SwitchNavMeshAgentMechanics(_agent);
            _agentFollowMechanics = new AgentFollowMechanics(_targetPosition, _followCondition, _agent);
        }

        public void OnEnable()
        {
            _switchNavMeshAgentMechanics.OnEnable();
        }
        
        public void Update()
        {
            _agentFollowMechanics.Update();
        }

        public void OnDisable()
        {
            _switchNavMeshAgentMechanics.OnDisable();
        }
    }
}