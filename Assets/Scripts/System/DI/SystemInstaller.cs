using Atomic.Objects;
using UnityEngine;
using Zenject;

namespace System
{
    internal sealed class SystemInstaller : MonoInstaller
    {
        [SerializeField]
        private AtomicObject _character;

        [SerializeField]
        private AtomicObject _gun;
        
        [SerializeField] 
        private Camera _camera;
        
        [SerializeField]
        private int _characterRotationTimeInMilliseconds;
        
        public override void InstallBindings()
        {
            BindMoveController();
            BindShootController();
            BindRotationController();
            BindFinishGameController();
        }

        private void BindMoveController()
        {
            Container.BindInterfacesTo<MoveInputController>().AsSingle().WithArguments(_character);
        }

        private void BindShootController()
        {
            Container.BindInterfacesTo<ShootInputController>().AsSingle().WithArguments(_gun, _characterRotationTimeInMilliseconds);
        }

        private void BindRotationController()
        {
            Container.BindInterfacesTo<RotateInputController>().AsSingle().WithArguments(_camera, _character.transform, _character);
        }
        
        private void BindFinishGameController()
        {
            Container.BindInterfacesTo<FinishGameObserver>().AsSingle().WithArguments(_character);
        }
    }
}