namespace Base.Model
{
    public class Shotgun : Gun
    {
        public override GunType GunType => GunType.Shotgun;
        public override uint MaxAmmo => 3;
        public override float Damage => 4;
    }
}