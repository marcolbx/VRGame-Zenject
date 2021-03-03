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
        
        [Inject]
        public void Init(WeaponController weaponController, SignalBus bus)
        {
            _weaponController = weaponController;
            bus.Subscribe<WeaponReload>(RefreshAmmoText);
            RefreshAmmoText();
        }

        private void RefreshAmmoText()
        {
            _handgunAmmoText.text = _weaponController.Inventory.HandgunAmmo.ToString();
            _shotgunAmmoText.text = _weaponController.Inventory.ShotgunAmmo.ToString();
            _machinegunAmmoText.text = _weaponController.Inventory.MachinegunAmmo.ToString();
        }
    }
}