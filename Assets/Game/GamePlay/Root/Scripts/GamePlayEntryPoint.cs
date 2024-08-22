using R3;
using EntryPoint;
using UnityEngine;

public class GamePlayEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGamePlayRootBinder _sceneUIRootPrefab;

    internal Observable<GamePlayExitParams> Run(UIRootView uIRoot, GamePlayEnterParams enterParams)
    {
        Debug.Log("GamePlay Scene is Loaded!");

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