using UnityEngine;

namespace Base.Controller
{
    public class ControlsController
    {
        public bool MobileInput => Input.touchCount > 0;
        public bool MobileWithoutButton => true;
        private bool _editorInput => Input.GetButtonDown("Fire1");
        public uint CurrentControl { get; set; } = 0;

        public bool InputCondition()
        {
    #if UNITY_EDITOR
            return  _editorInput;
    #else
        if (CurrentControl == ControlType.MobileWithButton)
            return MobileInput;
        else if (CurrentControl == ControlType.MobileWithoutButton)
            return MobileWithoutButton;
        else
            return MobileWithoutButton;
    #endif
        }

        public void SetControlsToMobileInputWithButton()
        {
            CurrentControl = ControlType.MobileWithButton;
            SaveManager.Instance.SaveControls();
        }

        public void SetControlsToMobileInputWithoutButton()
        {
            CurrentControl = ControlType.MobileWithoutButton;
            SaveManager.Instance.SaveControls();
        }
    }
}
