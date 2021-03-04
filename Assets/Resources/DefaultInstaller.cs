using Base.Controller;
using Base.Model;
using Base.Signal;
using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    [SerializeField] Ranks _rankList;
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
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
        Container.BindInterfacesAndSelfTo<RankController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SceneTransitionHandler>().AsSingle().NonLazy();
    }

    private void BindModels()
    {
        Container.Bind<WeaponInventory>().AsSingle();
        Container.Bind<Handgun>().AsSingle().NonLazy();
        Container.Bind<Shotgun>().AsSingle().NonLazy();
        Container.Bind<Machinegun>().AsSingle().NonLazy();
        Container.Bind<Player>().AsSingle().NonLazy();
    }

    private void BindScriptableObjects()
    {
        Container.BindInstance(_rankList).AsSingle();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<WeaponShoot>();
        Container.DeclareSignal<WeaponReloaded>();
        Container.DeclareSignal<WeaponEquipped>();
        Container.DeclareSignal<WeaponAmmoGained>();
        Container.DeclareSignal<PlayerDamaged>();
    }
}