
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
                _corutines.StartCoroutine(LoadingAndStartGamePlay());
                return;
            }

            if (sceneName != Scenes.BOOT)
                return;
#endif
            _corutines.StartCoroutine(LoadingAndStartMainMenu());

        }

        private IEnumerator LoadingAndStartMainMenu()
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAINMENU);

            yield return new WaitForSeconds(2); // На всякий случай ждем две скунды...

            ///
            //var sceneEntryPoint = UnityEngine.Object.FindFirstObjectByType<MainMenuEntryPoint>();
            //sceneEntryPoint.Run(_uiRoot);
            
            _uiRoot.HideLoadingScreen();
        }
        
        private IEnumerator LoadingAndStartGamePlay()
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(2); // На всякий случай ждем две скунды...

            ///
            //var sceneEntryPoint = UnityEngine.Object.FindFirstObjectByType<GamePlayEntryPoint>();
            //sceneEntryPoint.Run(_uiRoot);
            
            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}