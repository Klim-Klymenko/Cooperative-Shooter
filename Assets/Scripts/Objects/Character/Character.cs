using Atomic.Elements;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;

namespace Objects
{
    [Is(ObjectTypes.Character, ObjectTypes.Damageable, ObjectTypes.Movable)]
    internal sealed class Character : AtomicObject, IInitializeGameListener, IUpdateGameListener, IFinishGameListener
    {
        [Get(LiveableAPI.HitPoints)] 
        private IAtomicValue<int> CurrentHitPoints => _core.CurrentHitPoints;
        
        [Get(LiveableAPI.TakeDamageAction)]
        private IAtomicAction<int> TakeDamageAction => _core.TakeDamageEvent;
        
        [Get(LiveableAPI.DeathObservable)]
        private IAtomicObservable DeathObservable => _core.DeathObservable;
        
        [Get(MovableAPI.MovementDirection)]
        private IAtomicVariable<Vector3> MovementDirection => _core.MovementDirection;

        [Get(MovableAPI.MoveCondition)]
        private IAtomicValue<bool> MoveCondition => _core.MoveCondition;
        
        [Get(RotatableAPI.RotationDirection)]
        private IAtomicVariable<Vector3> RotationDirection => _core.RotationDirection;
        
        [SerializeField]
        private Character_Core _core;
        
        [SerializeField]
        private Character_Animation _animation;

        [SerializeField]
        private Character_Audio _audio;

        [SerializeField] 
        private Character_Particle _particle;
        
        public override void Compose()
        {
            base.Compose();
            
            _core.Compose();
            _animation.Compose(_core);
            _audio.Compose(_core);
            _particle.Compose(_core);
            
            _core.OnEnable();
            _animation.OnEnable();
            _audio.OnEnable();
        }

        void IInitializeGameListener.OnInitialize()
        {
            Compose();
        }

        void IUpdateGameListener.OnUpdate()
        {
            _core.Update();
            _animation.Update();
            _audio.Update();
            _particle.Update();
        }

        void IFinishGameListener.OnFinish()
        {
            _core.OnDisable();
            _animation.OnDisable();
            _audio.OnDisable();
            
            _core.Dispose();
            _audio.Dispose();
            _particle.Dispose();
        }
    }
}