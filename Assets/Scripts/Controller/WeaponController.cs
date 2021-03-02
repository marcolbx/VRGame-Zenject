using System.Threading.Tasks;
using Base.Model;
using Base.Signal;
using UnityEngine;
using Zenject;

namespace Base.Controller
{
    public enum GunType
    {
        Handgun,
        Shotgun,
        Machinegun
    }

    public class WeaponController
    {
        private SignalBus _bus;
        private WeaponInventory _inventory;
        public IGun CurrentGun {get; private set;}
        private Handgun _handgun;
        public bool IsPlayerReloading {get; private set;}

        public WeaponController(WeaponInventory inventory, Handgun handgun, SignalBus bus)
        {
            _bus = bus;
            _inventory = inventory;
            _handgun = handgun;
            _handgun.CurrentAmmo = 10;
            CurrentGun = _handgun;
        }

        public bool CanShoot()
        {
            return !CurrentGun.IsMagazineEmpty;
        }

        public void Shoot()
        {
            if (!CanShoot())
                return;
            
            _bus.Fire(new WeaponShoot());
            CurrentGun.Shoot();
        }

        public void GainAmmo(GunType gun)
        {
            if (GunType.Handgun == gun)
            {
                _inventory.HandgunAmmo += 5;
            }
            else if (GunType.Shotgun == gun)
            {
                _inventory.ShotgunAmmo += 2;
            }
            else
            {
                _inventory.MachinegunAmmo += 3;
            }
        }

        private bool CanReload()
        {
            if (!CurrentGun.IsMagazineEmpty)
                return false;

            if (GunType.Handgun == CurrentGun.GunType)
            {
                return _inventory.HandgunAmmo > 0;
            }
            else if (GunType.Shotgun == CurrentGun.GunType)
            {
                return _inventory.ShotgunAmmo > 0;
            }
            else
            {
                return _inventory.MachinegunAmmo > 0;
            }
        }

        public void Reload()
        {
            if (!CanReload())
                return;

            if (GunType.Handgun == CurrentGun.GunType)
            {
                ReloadHandgun();
            }
            else if (GunType.Shotgun == CurrentGun.GunType)
            {
                ReloadShotgun();
            }
            else
            {
                ReloadMachinegun();
            }

            IsPlayerReloading = true;
            DoReload();
        }

        private async void DoReload()
        {
            await Task.Delay(1000); // TODO reload based on gun time
            IsPlayerReloading = false;
        }

        private void ReloadHandgun()
        {
            if (_inventory.HandgunAmmo >= CurrentGun.MaxAmmo)
            {
                CurrentGun.CurrentAmmo = CurrentGun.MaxAmmo;
                _inventory.HandgunAmmo -= CurrentGun.MaxAmmo;
            }
            else
            {
                CurrentGun.CurrentAmmo = _inventory.HandgunAmmo;
                _inventory.HandgunAmmo = 0;
            }
        }

        private void ReloadShotgun()
        {
            if (_inventory.ShotgunAmmo >= CurrentGun.MaxAmmo)
            {
                CurrentGun.CurrentAmmo = CurrentGun.MaxAmmo;
                _inventory.ShotgunAmmo -= CurrentGun.MaxAmmo;
            }
            else
            {
                CurrentGun.CurrentAmmo = _inventory.ShotgunAmmo;
                _inventory.ShotgunAmmo = 0;
            }
        }

        private void ReloadMachinegun()
        {
            if (_inventory.MachinegunAmmo >= CurrentGun.MaxAmmo)
            {
                CurrentGun.CurrentAmmo = CurrentGun.MaxAmmo;
                _inventory.MachinegunAmmo -= CurrentGun.MaxAmmo;
            }
            else
            {
                CurrentGun.CurrentAmmo = _inventory.MachinegunAmmo;
                _inventory.MachinegunAmmo = 0;
            }
        }

        public void ChangeWeapon(IGun gun)
        {
            if (IsPlayerReloading)
                return;

            CurrentGun = gun;
        }
    }
}