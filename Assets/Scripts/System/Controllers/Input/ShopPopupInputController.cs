using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common.LocalInput;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UI.Managers;

namespace System
{
    [UsedImplicitly]
    internal sealed class ShopPopupInputController : IStartGameListener, IUpdateGameListener
    {
        private IAtomicValue<bool> _aliveCondition;
        
        private readonly IAtomicObject _character;
        private readonly InputFacade _input;
        private readonly IPopupManager _popupManager;

        internal ShopPopupInputController(IAtomicObject character, InputFacade input, IPopupManager popupManager)
        {
            _character = character;
            _input = input;
            _popupManager = popupManager;
        }


        void IStartGameListener.OnStart()
        {
            _aliveCondition = _character.GetValue<bool>(LiveableAPI.AliveCondition);
        }

        void IUpdateGameListener.OnUpdate()
        {
            if (!_aliveCondition.Value) return;
            
            if (_input.OpenShopMenuButton)
                _popupManager.Show();
            
            else if (_input.CloseShopMenuButton)
                _popupManager.Hide();
        }
    }
}