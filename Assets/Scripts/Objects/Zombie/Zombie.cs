using Atomic.Elements;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Is(ObjectTypes.Zombie, ObjectTypes.Damageable, ObjectTypes.Attacker, ObjectTypes.NavMeshAgent)]
    internal sealed class Zombie : AtomicObject, IUpdateGameListener, IFinishGameListener
    {
        [Get(LiveableAPI.HitPoints)] 
        private IAtomicValue<int> CurrentHitPoints => _core.CurrentHitPoints;
        
        [Get(LiveableAPI.TakeDamageAction)]
        private IAtomicAction<int> TakeDamageAction => _core.TakeDamageAction;
        
        [Get(LiveableAPI.DeathObservable)]
        private IAtomicObservable DeathObservable => _core.DeathObservable;

        [Get(AttackerAPI.TargetTransform)] 
        private readonly AtomicVariable<Transform> _targetTransform = new();

        [Get(AttackerAPI.AttackAction)]
        private IAtomicAction<IAtomicObject> AttackAction => _core.AttackAction;

        [Get(VisualAPI.SkinnedMeshRenderer)]
        private SkinnedMeshRenderer SkinnedMeshRenderer => _animation.SkinnedMeshRenderer;
        
        [SerializeField]
        [Get(ZombieAPI.ZombieAnimatorDispatcher)]
        private ZombieAnimatorDispatcher _zombieAnimatorDispatcher;
        
        [SerializeField]
        private Zombie_Core _core;
        
        [SerializeField]
        private Zombie_AI _ai;
        
        [SerializeField]
        private Zombie_Animation _animation;
        
        [SerializeField]
        private Zombie_Audio _audio;

        private bool _composed;
        
        public override void Compose()
        {
            base.Compose();

            _core.Compose(_targetTransform);
            _ai.Compose(_core);
            _animation.Compose(_core, _ai);
            _audio.Compose(_core);
            
            _core.OnEnable();
            _ai.OnEnable();
            _animation.OnEnable();
            _audio.OnEnable();

            _composed = true;
        }
        
        void IUpdateGameListener.OnUpdate()
        {
            if (!_composed) return;
            
            _core.Update();
            _ai.Update();
            _animation.Update();
        }

        public void OnFinish()
        {
            if (!_composed) return;
            
            _core.OnDisable();
            _ai.OnDisable();
            _animation.OnDisable();
            _audio.OnDisable();
            
            _core?.Dispose();
        }
    }
}