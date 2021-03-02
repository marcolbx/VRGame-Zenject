using System.Text;
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
        public void Init(WeaponController weaponController, SignalBus bus)
        {
            _weaponController = weaponController;
            bus.Subscribe<WeaponShoot>(OnWeaponActionRefreshText);
            bus.Subscribe<WeaponReload>(OnWeaponActionRefreshText);
        }

        private void OnWeaponActionRefreshText()
        {
            _textMeshProUGUI.text = _weaponController.CurrentGun.CurrentAmmo.ToString();
        }
    }
}