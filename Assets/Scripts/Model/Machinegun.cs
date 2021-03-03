namespace Base.Model
{
    public class Machinegun : Gun
    {
        public override GunType GunType => GunType.Machinegun;
        public override uint MaxAmmo => 15;
    }
}