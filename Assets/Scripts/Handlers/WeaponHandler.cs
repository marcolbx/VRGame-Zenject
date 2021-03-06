using Base.Controller;
using Base.Model;
using UnityEngine;
using Zenject;

namespace Base.Handler
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private GunType _gunType;
        [SerializeField] private GameObject _attachment;
        private WeaponController _weaponController;

        [Inject]
        public void Init(WeaponController weaponController, SignalBus bus)
        {
            _weaponController = weaponController;
        }

        private void Start() 
        {
            EnablePurchasedAttachments();
        }

        private void EnablePurchasedAttachments()
        {
            if (_gunType == GunType.Handgun && _weaponController.Handgun.HasAttachment)
            {
                _attachment.SetActive(true);
            }
            else if (_gunType == GunType.Shotgun && _weaponController.Shotgun.HasAttachment)
            {
                _attachment.SetActive(true);
            }
            else if (_gunType == GunType.Machinegun && _weaponController.Machinegun.HasAttachment)
            {
                _attachment.SetActive(true);
            }
        }
    }
}