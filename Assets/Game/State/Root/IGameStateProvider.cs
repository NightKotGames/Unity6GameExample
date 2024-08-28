
using R3;

namespace StateGame
{
    internal interface IGameStateProvider
    {
        GameStateProxy GameState { get; }
        GameSettingsStateProxy GameSettingsState {  get; }
        Observable<GameStateProxy> LoadGameState();
        Observable<GameSettingsStateProxy> LoadGameSettingState();
        Observable<bool> SaveGameState();
        Observable<bool> SaveGameSettingsState();
        Observable<bool> ResetGameState();
        Observable<bool> ResetGameSettingsState();
    }
}