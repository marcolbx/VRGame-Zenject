using Base.Signal;
using UnityEngine;
using Zenject;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _itemBoughtSound;
    [SerializeField] private AudioSource _controlSelectedSound;

    [Inject]
    public void Init(SignalBus bus)
    {
        bus.Subscribe<AttachmentBought>(OnAttachmentBought);
        bus.Subscribe<ControlSelected>(OnControlSelected);
    }

    private void OnAttachmentBought()
    {
        _itemBoughtSound.Play();
    }

    private void OnControlSelected()
    {
        _controlSelectedSound.Play();
    }
}
