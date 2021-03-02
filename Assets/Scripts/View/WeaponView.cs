using Base.Controller;
using Base.Signal;
using TMPro;
using UnityEngine;
using Zenject;

namespace Base.View
{
    public class WeaponView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        private WeaponController _weaponController;
        
        [Inject]
        public void Init(WeaponController _weaponController, SignalBus bus)
        {
            bus.Subscribe<WeaponShoot>(OnWeaponShootRefreshText);
        }

        private void OnWeaponShootRefreshText()
        {
            _textMeshProUGUI.text = _weaponController.CurrentGun.CurrentAmmo.ToString();
        }
    }
}