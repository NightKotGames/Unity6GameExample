
using DI;

public static class MainMenuRegistrations
{
    public static void Register(DIContainer container, MainMenuEnterParams @params)
    {
        // example
        //container.RegisterFactory(c => new SomeMainMenuService(c.Resolve<SomeCommonService>()));

        /// Регистрируем
        /// 

        container.RegisterFactory(c => new SomeMainMenuService(c.Resolve<SomeCommonService>())).AsSingle();

    }
}