using Base.Signal;
using UnityEngine;
using Zenject;

namespace Base.View
{
    public class StoreView : MonoBehaviour
    {
        public enum Attachment
        {
            HandgunRedDot,
            ShotgunScope,
            MachinegunScope
        }

        [SerializeField] private Attachment _attachment;
        private StoreController _storeController;

        [Inject]
        public void Init(StoreController storeController, SignalBus bus)
        {
            _storeController = storeController;
            bus.Subscribe<AttachmentBought>(CheckIfIsAlreadyBought);
        }

        private void Start() 
        {
            CheckIfIsAlreadyBought();
        }

        private void CheckIfIsAlreadyBought()
        {
            if (_attachment == Attachment.HandgunRedDot && _storeController.HasHandgunAttachment)
                gameObject.SetActive(false);
            else if (_attachment == Attachment.ShotgunScope && _storeController.HasShotgunAttachment)
                gameObject.SetActive(false);
            else if (_attachment == Attachment.MachinegunScope && _storeController.HasMachinegunAttachment)
                gameObject.SetActive(false);
        }

        public void OnPointerClick()
        {
            if (_attachment == Attachment.HandgunRedDot)
            {
               _storeController.BuyHandgunsRedDotSight();
            }
            else if (_attachment == Attachment.ShotgunScope)
            {
                _storeController.BuyShotgunsScope();
            }
            else if (_attachment == Attachment.MachinegunScope)
            {
                _storeController.BuyMachinegunScope();
            }
        }
    }
}