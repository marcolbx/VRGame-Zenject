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
        private WeaponController _weaponController; //TODO remove from here
        
        [Inject]
        public void Init(PlayerController playerController, SignalBus bus, WeaponController weaponController)
        {
            _playerController = playerController;
            bus.Subscribe<PlayerDamaged>(OnDamageRefreshText);

            //TODO remove
            _weaponController = weaponController;
        }

        private void Start() {
            _healthText.text = _playerController.Player.CurrentHealth.ToString();
        }

        private void Update() {
            if (Input.GetKeyDown("space"))
            {
                EquipShotgun();
            }
        }

        private void OnDamageRefreshText()
        {
            _healthText.text = _playerController.Player.CurrentHealth.ToString();
        }

        public void EquipShotgun()
        {
            _weaponController.ChangeToShotgun();
        }
    }
}