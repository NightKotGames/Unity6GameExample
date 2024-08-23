using R3;
using DI;
using EntryPoint;
using UnityEngine;

public class GamePlayEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGamePlayRootBinder _sceneUIRootPrefab;

    internal Observable<GamePlayExitParams> Run(DIContainer container, GamePlayEnterParams enterParams)
    {
        /// ����������� ��� ������� � �������� ��� ���� �����        
        GamePlayRegistrations.Register(container, enterParams);

        /// ������� � ������������ ����� ��������� ��� ViewModel
        var gamePlayViewModelContainer = new DIContainer(container);
        GamePlayViewModelRegistration.Register(gamePlayViewModelContainer);

        ///
        /// ��� ������:
        gamePlayViewModelContainer.Resolve<UIGamePlayRootViewModel>();
        gamePlayViewModelContainer.Resolve<WorldGamePlayRootViewModel>();

        Debug.Log("GamePlay Scene is Loaded!");

        var uIRoot = container.Resolve<UIRootView>();
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uIRoot.AttachSceneUI(uiScene.gameObject);

        // ������� ���� �������� ������

        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);

        Debug.Log($"GAMEPLAY ENTRY POINT: saveFileName = {enterParams.SaveFileName}, Level to Load = {enterParams.LevelNumber}");

        // ������� ����� �� �������� ��������� �����

        var mainMenuEnterParams = new MainMenuEnterParams("Vvariable params for Main Menu from GamePlay Scene...");
        var exitParams = new GamePlayExitParams(mainMenuEnterParams);

        // ��������������� Unit (exitSceneSignalSubj) � �������� ���
        
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }
}