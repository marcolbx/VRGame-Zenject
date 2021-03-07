using UnityEngine;
using Zenject;
using Base.Controller;
using Base.Signal;
using TMPro;

namespace Base.View
{
    public class ControlView : MonoBehaviour
    {
        enum ControlType
        {
            MobileWithoutButton,
            MobileWithButton,
        }

        [SerializeField] private ControlType _controlType;
        [SerializeField] private TextMeshProUGUI _controlText;
        [SerializeField] private TextMeshProUGUI _otherControlText;
        private Color _selectedColor => Color.green;
        private Color _defaultColor => Color.white;

        private ControlsController _controlsController;

        [Inject]
        public void Init(ControlsController controlsController, SignalBus bus)
        {
            _controlsController = controlsController;
            bus.Subscribe<DataLoaded>(OnDataLoaded);
        }

        public void OnPointerEnter()
        {
            if (_controlType == ControlType.MobileWithoutButton)
            {
                _controlsController.SetControlsToMobileInputWithoutButton();
                RefreshColors();
            }
        }

        public void OnPointerClick()
        {
            if (_controlType == ControlType.MobileWithButton)
            {
                _controlsController.SetControlsToMobileInputWithButton();
                RefreshColors();
            }
        }

        private void RefreshColors()
        {
            _controlText.color = _selectedColor;
            _otherControlText.color = _defaultColor;
        }

        private void OnDataLoaded()
        {
            if (_controlType == ControlType.MobileWithoutButton &&  _controlsController.CurrentControl == 0)
            {
                _controlText.color = _selectedColor;
            }
            else if (_controlType == ControlType.MobileWithButton && _controlsController.CurrentControl == 1)
            {
                _controlText.color = _selectedColor;
            }
        }
    }
}