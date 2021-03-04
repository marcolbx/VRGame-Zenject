using Base.Controller;
using Base.Signal;
using TMPro;
using UnityEngine;
using Zenject;

namespace Base.View
{
    public class PlayerView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _healthText;
        private PlayerController _playerController;

        [Inject]
        public void Init(PlayerController playerController, SignalBus bus)
        {
            _playerController = playerController;
            bus.Subscribe<PlayerDamaged>(OnDamageRefreshText);
        }

        private void Start() {
            _healthText.text = _playerController.Player.CurrentHealth.ToString();
        }

        private void OnDamageRefreshText()
        {
            _healthText.text = _playerController.Player.CurrentHealth.ToString();
        }
    }
}