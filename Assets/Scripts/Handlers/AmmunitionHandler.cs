using Base.Model;
using Base.Signal;
using UnityEngine;
using Zenject;

namespace Base.Handler
{
    public class AmmunitionHandler : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private GunType _ammoType;
        [SerializeField] private uint bullets;
        private WeaponInventory _weaponInventory;
        private SignalBus _bus;

        [Inject]
        public void Init(WeaponInventory weaponInventory, SignalBus bus)
        {
            _weaponInventory = weaponInventory;
            _bus = bus;
        }

        public void AddAmmo()
        {
            if (_ammoType == GunType.Handgun)
                _weaponInventory.HandgunAmmo += bullets;
            else if (_ammoType == GunType.Shotgun)
                _weaponInventory.ShotgunAmmo += bullets;
            else if (_ammoType == GunType.Machinegun)
                _weaponInventory.MachinegunAmmo += bullets;

            _bus.Fire(new WeaponAmmoGained());
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AddAmmo();
                _collider.enabled = false;
                gameObject.SetActive(false);
            }
        }

    }
}
