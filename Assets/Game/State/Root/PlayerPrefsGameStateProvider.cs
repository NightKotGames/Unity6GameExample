
using R3;
using Newtonsoft.Json;

namespace StateGame
{
    internal class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        private readonly string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        private readonly string GAME_SETTINGS_STATE_KEY = nameof(GAME_SETTINGS_STATE_KEY);

        private GameState _gameStateOrigin;
        private GameStateProxy _gameState;
        public GameStateProxy GameState => _gameState;

        private GameSettingsStateProxy _gameSettingsState;
        public GameSettingsStateProxy GameSettingsState => _gameSettingsState;

        public Observable<GameStateProxy> LoadGameState()
        {
            UnityEngine.Debug.Log(GAME_STATE_KEY);

            if (UnityEngine.PlayerPrefs.HasKey(GAME_STATE_KEY) == true)
            {
                string _json = UnityEngine.PlayerPrefs.GetString(GAME_STATE_KEY);
                JsonConvert.PopulateObject(_json, _gameStateOrigin);
                _gameState = new GameStateProxy(_gameStateOrigin);

                UnityEngine.Debug.Log($"GameState Created from Settings: " +
                                      $"{JsonConvert.SerializeObject(_gameState)} " +
                                      $"from Patch: \n{GAME_STATE_KEY}");

            }
            else
            {
                _gameState = CreateGameStateFromSettings();
                UnityEngine.Debug.Log($"GameState Created from Settings: {JsonConvert.SerializeObject(_gameStateOrigin, Formatting.Indented)}");
                SaveGameState();
            }

            return Observable.Return(_gameState);
        }

        public Observable<bool> SaveGameState()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_gameState, Formatting.Indented);

                UnityEngine.Debug.Log($"GameState Saved: " +
                                      $"{JsonConvert.SerializeObject(_gameState)} " +
                                      $"from Patch: \n{GAME_STATE_KEY}");

                UnityEngine.PlayerPrefs.SetString(GAME_STATE_KEY, json);
                return Observable.Return(true);
            }
            catch (System.Exception err)
            {
                UnityEngine.Debug.Log(err);
                return Observable.Return(false);
            }
        }

        public Observable<bool> ResetGameState()
        {
            try
            {
                _gameState = CreateGameStateFromSettings();
                SaveGameState();
                return Observable.Return(true);
            }
            catch (System.Exception err)
            {
                UnityEngine.Debug.LogWarning(err);
                return Observable.Return(false);
            }
        }

        private GameStateProxy CreateGameStateFromSettings()
        {
            _gameStateOrigin = new GameState
            {
                Buildings = new System.Collections.Generic.List<BuildingEntity>()
                {
                    new()
                    {
                        TypeId = "default_00"
                    },
                    new()
                    {
                        TypeId = "default_01"
                    }
                }
            };
            return new GameStateProxy(_gameStateOrigin);
        }

        public Observable<GameSettingsStateProxy> LoadGameSettingState()
        {
            throw new System.NotImplementedException();
        }

        public Observable<bool> SaveGameSettingsState()
        {
            throw new System.NotImplementedException();
        }

        public Observable<bool> ResetGameSettingsState()
        {
            throw new System.NotImplementedException();
        }
    }
}