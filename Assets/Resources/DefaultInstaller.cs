using Base.Controller;
using Base.Model;
using Base.Signal;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        BindModels();
        BindControllers();
        DeclareSignals();
    }

    private void BindControllers()
    {
        Container.BindInterfacesAndSelfTo<WeaponController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
        Container.BindInterfacesAndSelfTo<SceneTransitionHandler>().AsSingle();
    }

    private void BindModels()
    {
        Container.Bind<WeaponInventory>().AsSingle();
        Container.Bind<Handgun>().AsSingle().NonLazy();
        Container.Bind<Shotgun>().AsSingle().NonLazy();
        Container.Bind<Player>().AsSingle().NonLazy();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<WeaponShoot>();
        Container.DeclareSignal<WeaponReload>();
        Container.DeclareSignal<PlayerDamaged>();
    }
}