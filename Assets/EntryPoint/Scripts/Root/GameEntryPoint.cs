
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

        // ������� ����������
        private readonly DIContainer _rootContainer = new();
        private DIContainer _cachetSceneContainer;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] public static void AutoStartGame()
        {
            // ������������� ����� �������� ����� �� ��������� ���������. ��������:
            Application.targetFrameRate = 60;

            // ������
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


            // ������������ ��������� ������� 
            _rootContainer.RegisterInstance(_uiRoot);

            // ��������� �� ������� ����� Factory
            _rootContainer.RegisterFactory(_ => new SomeCommonService()).AsSingle();

            /// ����������� � ��������� ������� ��������:
            /// ������� �����������, ��������� � �.�...
            
            

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
                // ����� �� �������� ���������
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
            // ���� ����������� �������, ���� ��������� ������ ���������� - ������� ���
            _cachetSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAINMENU);

            yield return new WaitForSeconds(1); // �� ������ ������ ����...

            ///
            var sceneEntryPoint = UnityEngine.Object.FindFirstObjectByType<MainMenuEntryPoint>();
            
            /// ������� ��������� ������� ����� (�������� - ��������� �������)
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

            // ���� ����������� �������, ���� ��������� ������ ���������� - ������� ���
            _cachetSceneContainer?.Dispose();

            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);

            yield return new WaitForSeconds(1); // �� ������ ������ ����...

            ///
            var sceneEntryPoint = UnityEngine.Object.FindFirstObjectByType<GamePlayEntryPoint>();

            /// ������� ��������� ������� ����� (�������� - ��������� �������)
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