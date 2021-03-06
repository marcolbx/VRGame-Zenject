using System.Collections.Generic;

namespace Base.Model
{
    public enum GunType
    {
        Handgun,
        Shotgun,
        Machinegun
    }

    public interface IGun
    {
        GunType GunType { get; }
        uint MaxAmmo { get; }
        uint CurrentAmmo { get; set;}
        bool IsMagazineEmpty { get; }
        bool IsMagazineFull { get; }
        float Damage { get; set; }
        bool HasAttachment { get; }

        void Shoot();
    }

    public abstract class Gun : IGun
    {
        public virtual GunType GunType { get; set; }
        public virtual uint MaxAmmo { get; set; } //Maximum quantity of bullets that can be loaded in the gun.
        public uint CurrentAmmo { get; set; }//Current amount of bullets loaded inside gun
        public bool IsMagazineEmpty => CurrentAmmo == 0;
        public bool IsMagazineFull => CurrentAmmo == MaxAmmo;
        public virtual float Damage { get; set; } = 1;
        public virtual bool HasAttachment => Attachments?.Count > 0;
        public List<Attachment> Attachments { get; private set; } = new List<Attachment>();

        public virtual void Shoot()
        {
            CurrentAmmo--;
        }
    }
}

