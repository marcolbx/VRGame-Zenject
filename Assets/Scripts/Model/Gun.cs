using Base.Controller;

namespace Base.Model
{
    public interface IGun
    {
        GunType GunType { get; }
        uint MaxAmmo { get; }
        uint CurrentAmmo { get; set;}
        bool IsMagazineEmpty { get; }
        bool IsMagazineFull { get; }
        void Shoot();
    }

    public abstract class Gun : IGun
    {
        public virtual GunType GunType { get; set; }
        public uint MaxAmmo { get; private set; } //Maximum quantity of bullets that can be loaded in the gun.
        public uint CurrentAmmo { get; set; }//Current amount of bullets loaded inside gun
        public bool IsMagazineEmpty => CurrentAmmo == 0;
        public bool IsMagazineFull => CurrentAmmo == MaxAmmo;

        public virtual void Shoot()
        {
            CurrentAmmo--;
        }
    }
}

