﻿
using DI;
using StateGame;

public static class GamePlayRegistrations
{
    public static void Register(DIContainer container, GamePlayEnterParams @params)
    {
        container.RegisterFactory(c => new SomeGamePlayService(c.Resolve<IGameStateProvider>().GameState , c.Resolve<SomeCommonService>())).AsSingle();


    }
}