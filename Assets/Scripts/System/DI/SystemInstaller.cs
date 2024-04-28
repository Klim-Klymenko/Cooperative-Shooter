using Atomic.Objects;
using Common.LocalInput;
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
            BindInput();
            BindInputControllers();
            BindFinishGameController();
        }

        private void BindInput()
        {
            Container.Bind<InputFacade>().AsSingle();
        }
        
        private void BindInputControllers()
        {
            Container.BindInterfacesTo<MoveInputController>().AsSingle().WithArguments(_character);
            Container.BindInterfacesTo<ShootInputController>().AsSingle().WithArguments(_gun, _characterRotationTimeInMilliseconds);
            Container.BindInterfacesTo<RotateInputController>().AsSingle().WithArguments(_camera, _character.transform, _character);
        }
        
        private void BindFinishGameController()
        {
            Container.BindInterfacesTo<FinishGameObserver>().AsSingle().WithArguments(_character);
        }
    }
}