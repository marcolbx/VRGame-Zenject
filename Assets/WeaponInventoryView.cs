using Base.Controller;
using Base.Signal;
using TMPro;
using UnityEngine;
using Zenject;

namespace Base.View
{
    public class WeaponInventoryView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _handgunAmmoText;
        private WeaponController _weaponController;
        
        [Inject]
        public void Init(WeaponController weaponController, SignalBus bus)
        {
            _weaponController = weaponController;
            bus.Subscribe<WeaponReload>(OnWeaponReloadRefreshText);
            _handgunAmmoText.text = _weaponController.Inventory.HandgunAmmo.ToString();
        }

        private void OnWeaponReloadRefreshText()
        {
            _handgunAmmoText.text = _weaponController.Inventory.HandgunAmmo.ToString();
        }
    }
}