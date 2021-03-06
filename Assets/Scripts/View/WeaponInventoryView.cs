using Base.Controller;
using Base.Signal;
using TMPro;
using UnityEngine;
using Zenject;

namespace Base.View
{
    public class WeaponInventoryView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _handgunAmmoText;
        [SerializeField] private TextMeshProUGUI _shotgunAmmoText;
        [SerializeField] private TextMeshProUGUI _machinegunAmmoText;
        private WeaponController _weaponController;
        private readonly Color _noAmmoLeftColor = Color.red;
        private readonly Color _hasAmmoColor = Color.white;

        
        [Inject]
        public void Init(WeaponController weaponController, SignalBus bus)
        {
            _weaponController = weaponController;
            bus.Subscribe<WeaponReloaded>(RefreshAmmoText);
            bus.Subscribe<WeaponAmmoGained>(RefreshAmmoText);
            RefreshAmmoText();
        }

        private void RefreshAmmoText()
        {
            _handgunAmmoText.text = _weaponController.Inventory.HandgunAmmo.ToString();
            _shotgunAmmoText.text = _weaponController.Inventory.ShotgunAmmo.ToString();
            _machinegunAmmoText.text = _weaponController.Inventory.MachinegunAmmo.ToString();

            ChangeColor();
        }

        private void ChangeColor()
        {
            if (_weaponController.Inventory.HandgunAmmo > 0)
                _handgunAmmoText.color = _hasAmmoColor;
            else
                _handgunAmmoText.color = _noAmmoLeftColor;

            if (_weaponController.Inventory.ShotgunAmmo > 0)
                _shotgunAmmoText.color = _hasAmmoColor;
            else
                _shotgunAmmoText.color = _noAmmoLeftColor;

            if (_weaponController.Inventory.MachinegunAmmo > 0)
                _machinegunAmmoText.color = _hasAmmoColor;
            else
                _machinegunAmmoText.color = _noAmmoLeftColor;
        }
    }
}