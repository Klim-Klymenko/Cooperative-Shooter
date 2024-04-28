using JetBrains.Annotations;
using UnityEngine;

namespace Common.LocalInput
{
    [UsedImplicitly]
    public sealed class InputFacade
    {
        #region Constants

        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        private const int Mouse0 = 0;
        private const int Mouse1 = 1;

        private const KeyCode E = KeyCode.E;

        #endregion

        #region Values

        private bool IsM0Down => Input.GetMouseButtonDown(Mouse0);
        private bool IsM0 => Input.GetMouseButton(Mouse0);
        private bool IsM0Up => Input.GetMouseButtonUp(Mouse0);
        
        private bool IsM1Down => Input.GetMouseButtonDown(Mouse1);
        private bool IsM1 => Input.GetMouseButton(Mouse1);
        private bool IsM1Up => Input.GetMouseButtonUp(Mouse1);
        
        private bool IsEDown => Input.GetKeyDown(E);
        private bool IsE => Input.GetKey(E);
        private bool IsEUp => Input.GetKeyUp(E);
        
        public float HorizontalAxis => Input.GetAxisRaw(Horizontal);
        public float VerticalAxis => Input.GetAxisRaw(Vertical);

        public Vector3 MousePosition => Input.mousePosition;
        
        #endregion

        #region Actions

        public bool ShootingStartButton => IsM0Down;
        public bool ShootingContinueButton => IsM0;
        public bool ShootingStopButton => IsM0Up;

        #endregion
    }
}