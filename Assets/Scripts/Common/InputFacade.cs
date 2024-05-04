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
        
        private const KeyCode Alpha1 = KeyCode.Alpha1;
        private const KeyCode Alpha2 = KeyCode.Alpha2;
        private const KeyCode Alpha3 = KeyCode.Alpha3;

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
        
        private bool IsAlpha1Down => Input.GetKeyDown(Alpha1);
        private bool IsAlpha1 => Input.GetKey(Alpha1);
        private bool IsAlpha1Up => Input.GetKeyUp(Alpha1);
        
        private bool IsAlpha2Down => Input.GetKeyDown(Alpha2);
        private bool IsAlpha2 => Input.GetKey(Alpha2);
        private bool IsAlpha2Up => Input.GetKeyUp(Alpha2);
        
        private bool IsAlpha3Down => Input.GetKeyDown(Alpha3);
        private bool IsAlpha3 => Input.GetKey(Alpha3);
        private bool IsAlpha3Up => Input.GetKeyUp(Alpha3);
        
        public float HorizontalAxis => Input.GetAxisRaw(Horizontal);
        public float VerticalAxis => Input.GetAxisRaw(Vertical);

        public Vector3 MousePosition => Input.mousePosition;
        
        #endregion

        #region Actions

        public bool ShootingStartButton => IsM0Down;
        public bool ShootingContinueButton => IsM0;
        public bool ShootingStopButton => IsM0Up;
        
        public bool SwitchingSlot1Button => IsAlpha1Down;
        public bool SwitchingSlot2Button => IsAlpha2Down;
        public bool SwitchingSlot3Button => IsAlpha3Down;

        #endregion
    }
}