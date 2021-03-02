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
        Container.BindInterfacesAndSelfTo<SceneTransitionHandler>().AsSingle();
    }

    private void BindModels()
    {
        Container.Bind<WeaponInventory>().AsSingle();
        Container.Bind<Handgun>().AsSingle().NonLazy();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<WeaponShoot>();
    }
}