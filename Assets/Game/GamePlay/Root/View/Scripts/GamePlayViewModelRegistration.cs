
using DI;

public class GamePlayViewModelRegistration
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(c => new UIGamePlayRootViewModel(c.Resolve<SomeGamePlayService>())).AsSingle();
        container.RegisterFactory(c => new WorldGamePlayRootViewModel()).AsSingle();
    }
}