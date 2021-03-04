using Base.Handler;
using Base.Model;
using UnityEngine;

namespace Base.View
{
    public class WeaponSwitcherView : MonoBehaviour
    {
        [SerializeField] private GunType _interactionType;
        [SerializeField] private WeaponsHandler _weaponsHandler;

        public void OnPointerEnter()
        {
            if (_interactionType == GunType.Handgun)
            {
                _weaponsHandler.EquipHandgun();
            }
            else if (_interactionType == GunType.Shotgun)
            {
                _weaponsHandler.EquipShotgun();
            }
            else if (_interactionType == GunType.Machinegun)
            {
                _weaponsHandler.EquipMachinegun();
            }
        }
    }
}