using System.Threading.Tasks;
using Base.Model;
using Base.Signal;
using Zenject;

namespace Base.Controller
{
    public class WeaponController
    {
        private SignalBus _bus;
        public WeaponInventory Inventory { get; private set; }
        public IGun CurrentGun { get; private set; }
        private Handgun _handgun;
        private Shotgun _shotgun;
        private Machinegun _machinegun;
        public bool IsPlayerReloading {get; private set;}

        public WeaponController(WeaponInventory inventory, Handgun handgun, Shotgun shotgun, Machinegun machinegun, SignalBus bus)
        {
            _bus = bus;
            Inventory = inventory;
            _handgun = handgun;
            _shotgun = shotgun;
            _machinegun = machinegun;

            _handgun.CurrentAmmo = _handgun.MaxAmmo;
            _shotgun.CurrentAmmo = _shotgun.MaxAmmo;
            Inventory.HandgunAmmo = 12; //TODO change;

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

             CurrentGun.Shoot();
            _bus.Fire(new WeaponShoot());
        }

        public void GainAmmo(GunType gun)
        {
            if (GunType.Handgun == gun)
            {
                Inventory.HandgunAmmo += 5;
            }
            else if (GunType.Shotgun == gun)
            {
                Inventory.ShotgunAmmo += 2;
            }
            else
            {
                Inventory.MachinegunAmmo += 3;
            }
        }

        private bool CanReload()
        {
            if (!CurrentGun.IsMagazineEmpty)
                return false;

            if (GunType.Handgun == CurrentGun.GunType)
            {
                return Inventory.HandgunAmmo > 0;
            }
            else if (GunType.Shotgun == CurrentGun.GunType)
            {
                return Inventory.ShotgunAmmo > 0;
            }
            else
            {
                return Inventory.MachinegunAmmo > 0;
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
            _bus.Fire(new WeaponReload());
        }

        private void ReloadHandgun()
        {
            if (Inventory.HandgunAmmo >= CurrentGun.MaxAmmo)
            {
                CurrentGun.CurrentAmmo = CurrentGun.MaxAmmo;
                Inventory.HandgunAmmo -= CurrentGun.MaxAmmo;
            }
            else
            {
                CurrentGun.CurrentAmmo = Inventory.HandgunAmmo;
                Inventory.HandgunAmmo = 0;
            }
        }

        private void ReloadShotgun()
        {
            if (Inventory.ShotgunAmmo >= CurrentGun.MaxAmmo)
            {
                CurrentGun.CurrentAmmo = CurrentGun.MaxAmmo;
                Inventory.ShotgunAmmo -= CurrentGun.MaxAmmo;
            }
            else
            {
                CurrentGun.CurrentAmmo = Inventory.ShotgunAmmo;
                Inventory.ShotgunAmmo = 0;
            }
        }

        private void ReloadMachinegun()
        {
            if (Inventory.MachinegunAmmo >= CurrentGun.MaxAmmo)
            {
                CurrentGun.CurrentAmmo = CurrentGun.MaxAmmo;
                Inventory.MachinegunAmmo -= CurrentGun.MaxAmmo;
            }
            else
            {
                CurrentGun.CurrentAmmo = Inventory.MachinegunAmmo;
                Inventory.MachinegunAmmo = 0;
            }
        }

        public void ChangeWeapon(IGun gun)
        {
            if (IsPlayerReloading)
                return;

            CurrentGun = gun;

            _bus.Fire(new WeaponEquipped());
        }

        public void ChangeToHandgun()
        {
            ChangeWeapon(_handgun);
        }
        public void ChangeToShotgun()
        {
            ChangeWeapon(_shotgun);
        }

        public void ChangeToMachinegun()
        {
            ChangeWeapon(_machinegun);
        }
    }
}