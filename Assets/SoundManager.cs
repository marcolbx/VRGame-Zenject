using Base.Signal;
using UnityEngine;
using Zenject;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _itemBoughtSound;
    [SerializeField] private AudioSource _music;

    [Inject]
    public void Init(SignalBus bus)
    {
        bus.Subscribe<AttachmentBought>(OnAttachmentBought);
    }

    private void Start() 
    {
        _music.Play();
    }

    private void OnAttachmentBought()
    {
        _itemBoughtSound.Play();
    }
}
