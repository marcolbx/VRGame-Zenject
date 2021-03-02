
using Base.Controller;

namespace Base.Model
{
    public class Handgun : Gun
    {
        public override GunType GunType => GunType.Handgun;
    }
}