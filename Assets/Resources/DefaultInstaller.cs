using Base.Controller;
using Base.Model;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        BindModels();
        BindControllers();
    }

    private void BindControllers()
    {
        Container.BindInterfacesAndSelfTo<WeaponController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SceneTransitionHandler>().AsSingle();
    }

    private void BindModels()
    {
        Container.Bind<WeaponInventory>().AsSingle();
        Container.Bind<Handgun>().AsSingle();
    }
}