
using R3;
using System.IO;
using Newtonsoft.Json;

namespace StateGame
{
    internal class JsonGameStateProvider : IGameStateProvider
    {
        private readonly string _gameStateFilePath = $"{UnityEngine.Application.persistentDataPath}/_save.dat";
        private readonly string _gameSettingsFilePath = $"{UnityEngine.Application.persistentDataPath}/_settings.dat";

        private GameState _gameStateOrigin;
        private GameStateProxy _gameState;
        private GameSettingsStateProxy _gameSettingsState;
        public GameStateProxy GameState => _gameState;

        public GameSettingsStateProxy GameSettingsState => _gameSettingsState;

        public Observable<GameStateProxy> LoadGameState()
        {
            UnityEngine.Debug.Log(_gameStateFilePath);

            if (File.Exists(_gameStateFilePath))
            {
                string _json = File.ReadAllText(_gameStateFilePath);
                JsonConvert.PopulateObject(_json, _gameStateOrigin);
                _gameState = new GameStateProxy(_gameStateOrigin);

                UnityEngine.Debug.Log($"GameState Created from Settings: " +
                                      $"{JsonConvert.SerializeObject(_gameState)} " +
                                      $"from Patch: \n{_gameStateFilePath}");

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
                                      $"from Patch: \n{_gameStateFilePath}");

                File.WriteAllText(_gameStateFilePath, json);
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