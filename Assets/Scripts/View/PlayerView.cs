using Base.Controller;
using Base.Signal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Base.View
{
    public class PlayerView : MonoBehaviour 
    {
        [SerializeField] private Image[] _heartIcons;
        [SerializeField] private TextMeshProUGUI _currentMoneyText;

        private PlayerController _playerController;
        private uint _hurtCount = 2;

        [Inject]
        public void Init(PlayerController playerController, SignalBus bus)
        {
            _playerController = playerController;
            bus.Subscribe<PlayerDamaged>(OnDamageRefreshText);
            bus.Subscribe<AttachmentBought>(OnItemBought);

        }

        private void Start() 
        {
            if (_heartIcons != null)
            foreach (var icon in _heartIcons)
            {
                icon.enabled = true;
            }

            if (_currentMoneyText != null)
                _currentMoneyText.text = _playerController.Player.Money.ToString() + "$";
        }

        private void OnDamageRefreshText()
        {
            _heartIcons[_hurtCount].enabled = false;

            if( _hurtCount > 0)
                _hurtCount--;
        }

        private void OnItemBought()
        {
            if (_currentMoneyText != null)
                _currentMoneyText.text = _playerController.Player.Money.ToString() + "$";
        }
    }
}