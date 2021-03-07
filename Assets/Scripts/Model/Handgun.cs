namespace Base.Model
{
    public class Handgun : Gun
    {
        public override GunType GunType => GunType.Handgun;
        public override uint MaxAmmo => 10;
        public override float Damage => 1;
    }
}