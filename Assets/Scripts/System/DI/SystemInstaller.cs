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
        private Camera _camera;
        
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
            Container.BindInterfacesTo<MovementInputController>().AsSingle().WithArguments(_character);
            Container.BindInterfacesTo<ShootingInputController>().AsSingle().WithArguments(_character);
            Container.BindInterfacesTo<RotationInputController>().AsSingle().WithArguments(_camera, _character.transform, _character);
            Container.BindInterfacesTo<SwitchingGunController>().AsSingle().WithArguments(_character);
        }
        
        private void BindFinishGameController()
        {
            Container.BindInterfacesTo<FinishGameObserver>().AsSingle().WithArguments(_character);
        }
    }
}