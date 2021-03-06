using Base.Controller;
using Base.Handler;
using Base.Model;
using Base.Signal;
using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField] Ranks _rankList;
    [SerializeField] WeaponAttachments _weaponAttachments;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        BindScriptableObjects();
        BindModels();
        BindControllers();
        DeclareSignals();
    }

    private void BindControllers()
    {
        Container.BindInterfacesAndSelfTo<WeaponController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<StoreController>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
        Container.BindInterfacesAndSelfTo<RankController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SceneHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerStatsController>().AsSingle();
    }

    private void BindModels()
    {
        Container.Bind<WeaponInventory>().AsSingle();
        Container.Bind<Handgun>().AsSingle().NonLazy();
        Container.Bind<Shotgun>().AsSingle().NonLazy();
        Container.Bind<Machinegun>().AsSingle().NonLazy();
        Container.Bind<Player>().AsSingle().NonLazy();
        Container.Bind<PlayerStats>().AsSingle().NonLazy();
    }

    private void BindScriptableObjects()
    {
        Container.BindInstance(_rankList).AsSingle();
        Container.BindInstance(_weaponAttachments).AsSingle();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<WeaponShoot>();
        Container.DeclareSignal<WeaponReloadStart>();
        Container.DeclareSignal<WeaponReloaded>();
        Container.DeclareSignal<WeaponEquipped>();
        Container.DeclareSignal<WeaponAmmoGained>();
        Container.DeclareSignal<AttachmentBought>();
        Container.DeclareSignal<PlayerDamaged>();
    }
}