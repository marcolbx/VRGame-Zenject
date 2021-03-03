using Base.Controller;
using Base.Signal;
using TMPro;
using UnityEngine;
using Zenject;

namespace Base.View
{
    public class WeaponView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _currentAmmoText;
        [SerializeField] private TextMeshProUGUI _currentEquippedWeapon;
        private Color _handgunColor => Color.red;
        private Color _shotgunColor => Color.green;
        private Color _machinegunColor => Color.blue;
        private WeaponController _weaponController;
        
        [Inject]
        public void Init(WeaponController weaponController, SignalBus bus)
        {
            _weaponController = weaponController;
            bus.Subscribe<WeaponShoot>(OnWeaponActionRefreshText);
            bus.Subscribe<WeaponReload>(OnWeaponActionRefreshText);
            bus.Subscribe<WeaponEquipped>(OnWeaponEquipped);

            OnWeaponEquipped();
            OnWeaponActionRefreshText();
        }

        private void OnWeaponActionRefreshText()
        {
            _currentAmmoText.text = _weaponController.CurrentGun.CurrentAmmo.ToString();
        }

        private void OnWeaponEquipped()
        {
            _currentEquippedWeapon.text = _weaponController.CurrentGun.GunType.ToString();
            ChangeTextColorBasedOnWeapon();

            OnWeaponActionRefreshText();
        }

        private void ChangeTextColorBasedOnWeapon()
        {
            if (_weaponController.CurrentGun.GunType == Model.GunType.Handgun)
                _currentAmmoText.color = _handgunColor;
            else if (_weaponController.CurrentGun.GunType == Model.GunType.Shotgun)
                _currentAmmoText.color = _shotgunColor;
            else if (_weaponController.CurrentGun.GunType == Model.GunType.Machinegun)
                _currentAmmoText.color = _machinegunColor;
        }
    }
}