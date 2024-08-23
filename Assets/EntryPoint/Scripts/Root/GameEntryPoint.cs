
using DI;
using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace EntryPoint
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _corutines;
        private UIRootView _uiRoot;

        // Создаем контейнеры
        private readonly DIContainer _rootContainer = new();
        private DIContainer _cachetSceneContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] public static void AutoStartGame()
        {
            // Устанавливаем перед Запуском какие то системные настройки. Например:
            Application.targetFrameRate = 60;

            // Запуск
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _corutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            UnityEngine.Object.DontDestroyOnLoad(_corutines.gameObject);

            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = UnityEngine.Object.Instantiate(prefabUIRoot);
            UnityEngine.Object.DontDestroyOnLoad(_uiRoot.gameObject);


            // регистрируем контейнер проекта 
            _rootContainer.RegisterInstance(_uiRoot);

            // Создастся по запросу через Factory
            _rootContainer.RegisterFactory(_ => new SomeCommonService()).AsSingle();

            /// Опционально в контейнер проекта кладутся:
            /// Сервисы Монетизации, Анилитики и т.д...
            
            

        }

        private void RunGame()
        {

#if UNITY_EDITOR

            var sceneName = SceneManager.GetActiveScene().name;
            
            if(sceneName == Scenes.MAINMENU)
            {
                _corutines.StartCoroutine(LoadingAndStartMainMenu());
                return;
            }

            if (sceneName == Scenes.GAMEPLAY)
            {
                // какие то фейковые параметры
                var gamePlayEnterParams = new GamePlayEnterParams("default.save", 1);

                _corutines.StartCoroutine(LoadingAndStartGamePlay(gamePlayEnterParams));
                return;
            }

            if (sceneName != Scenes.BOOT)
                return;
#endif
            _corutines.StartCoroutine(LoadingAndStartMainMenu());

        }

        private IEnumerator LoadingAndStartMainMenu(MainMenuEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();
            // пока загружается уровень, если контейнер уровня существует - очищаем его
            _cachetSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAINMENU);

            yield return new WaitForSeconds(1); // На всякий случай ждем...

            ///
            var sceneEntryPoint = UnityEngine.Object.FindFirstObjectByType<MainMenuEntryPoint>();
            
            /// создаем контейнер игровой сцены (родитель - контейнер проекта)
            var mainMenuContainer = _cachetSceneContainer = new DIContainer(_rootContainer);     
            
            sceneEntryPoint.Run(mainMenuContainer, enterParams).Subscribe(mainMenuExitParams => 
            { 
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

                if (targetSceneName == Scenes.GAMEPLAY)
                    _corutines.StartCoroutine(LoadingAndStartGamePlay(mainMenuExitParams.TargetSceneEnterParams.As<GamePlayEnterParams>()));
            });

            ///
                       
            
            
            _uiRoot.HideLoadingScreen();
        }
        
        private IEnumerator LoadingAndStartGamePlay(GamePlayEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();

            // пока загружается уровень, если контейнер уровня существует - очищаем его
            _cachetSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(1); // На всякий случай ждем...

            ///
            var sceneEntryPoint = UnityEngine.Object.FindFirstObjectByType<GamePlayEntryPoint>();

            /// создаем контейнер игровой сцены (родитель - контейнер проекта)
            var gamePlayContainer = _cachetSceneContainer = new DIContainer(_rootContainer);

            sceneEntryPoint.Run(gamePlayContainer, enterParams).Subscribe(gamePlayExitParams => _corutines.StartCoroutine(LoadingAndStartMainMenu(gamePlayExitParams.MainMenuEnterParams)));

            ///



            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}