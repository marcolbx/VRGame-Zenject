using Base.Controller;
using Base.Model;
using Base.Signal;
using Zenject;

public class StoreController
{
    private WeaponController _weaponController;
    private Player _player;
    private WeaponAttachments _weaponAttachments;
    private SignalBus _bus;
    public bool HasHandgunAttachment => _weaponController.Handgun.HasAttachment;
    public bool HasShotgunAttachment => _weaponController.Shotgun.HasAttachment;
    public bool HasMachinegunAttachment => _weaponController.Machinegun.HasAttachment;

    public StoreController(WeaponController weaponController, Player player, WeaponAttachments weaponAttachments, SignalBus bus)
    {
        _weaponController = weaponController;
        _player = player;
        _weaponAttachments = weaponAttachments;
        _bus = bus;
    }

    public void BuyHandgunsRedDotSight()
    {
        if (_player.Money >= _weaponAttachments.Attachments[0].Cost)
        {
            _player.Money -= _weaponAttachments.Attachments[0].Cost;
            _weaponController.Handgun.Attachments.Add(new Attachment() { Name = _weaponAttachments.Attachments[0].Name });
            _bus.Fire(new AttachmentBought());
            SaveManager.Instance.SaveHandgunRedDotPurchase(_weaponAttachments.Attachments[0].Name);
        }
    }

    public void BuyShotgunsScope()
    {
        if (_player.Money >= _weaponAttachments.Attachments[1].Cost)
        {
            _player.Money -= _weaponAttachments.Attachments[1].Cost;
            _weaponController.Shotgun.Attachments.Add(new Attachment() { Name = _weaponAttachments.Attachments[1].Name });
            _bus.Fire(new AttachmentBought());
            SaveManager.Instance.SaveShotgunScopePurchase(_weaponAttachments.Attachments[1].Name);
        }
    }

    public void BuyMachinegunScope()
    {
        if (_player.Money >= _weaponAttachments.Attachments[2].Cost)
        {
            _player.Money -= _weaponAttachments.Attachments[2].Cost;
            _weaponController.Machinegun.Attachments.Add(new Attachment() { Name = _weaponAttachments.Attachments[2].Name });
            _bus.Fire(new AttachmentBought());
            SaveManager.Instance.SaveMachinegunScopePurchase(_weaponAttachments.Attachments[2].Name);
        }
    }
}
