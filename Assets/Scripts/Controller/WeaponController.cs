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
        public Handgun Handgun { get; private set; }
        public Shotgun Shotgun { get; private set; }
        public Machinegun Machinegun { get; private set; }
        private PlayerStatsController _playerStatsController;
        public bool HasAmmo => !CurrentGun.IsMagazineEmpty;
        public bool IsPlayerReloading { get; private set; }
        public bool IsShootable { get; private set; } = true;

        public WeaponController(WeaponInventory inventory, Handgun handgun, Shotgun shotgun, Machinegun machinegun, SignalBus bus, PlayerStatsController playerStatsController)
        {
            _bus = bus;
            Handgun = handgun;
            Shotgun = shotgun;
            Machinegun = machinegun;

            Inventory = inventory;
            CurrentGun = Handgun;

            _playerStatsController = playerStatsController;

            InitialAmmoSurvivalMode();
        }

        public void InitialAmmoSurvivalMode() //TODO if new game mode, change
        {
            Handgun.CurrentAmmo = Handgun.MaxAmmo;
            Shotgun.CurrentAmmo = Shotgun.MaxAmmo;
            Machinegun.CurrentAmmo = Machinegun.MaxAmmo;
            Inventory.HandgunAmmo = 50;
            Inventory.ShotgunAmmo = 30;
            Inventory.MachinegunAmmo = 100;
        }

        public bool CanShoot()
        {
            return IsShootable;
        }

        public void Shoot()
        {
            if (!CanShoot())
                return;

             CurrentGun.Shoot();
             IsShootable = false;

             DoFireRate();

             _playerStatsController.AddBulletShot();
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
            _bus.Fire(new WeaponReloadStart());
            DoReload();
        }

        private async void DoReload()
        {
            await Task.Delay(1000); // TODO reload based on gun time
            IsPlayerReloading = false;
            _bus.Fire(new WeaponReloaded());
        }

        private async void DoFireRate()
        {
            await Task.Delay(300);
            IsShootable = true;
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
            ChangeWeapon(Handgun);
        }
        public void ChangeToShotgun()
        {
            ChangeWeapon(Shotgun);
        }

        public void ChangeToMachinegun()
        {
            ChangeWeapon(Machinegun);
        }
    }
}