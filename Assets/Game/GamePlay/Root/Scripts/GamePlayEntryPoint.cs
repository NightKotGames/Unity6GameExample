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

        // Создаем Сабж выходных данных

        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);

        // Создаем выходные параметры

        var mainMenuEnterParams = new MainMenuEnterParams("variable params for Main Menu...");
        var exitParams = new GamePlayExitParams(mainMenuEnterParams);

        // Преобразовываем Unit (exitSceneSignalSubj) в
        
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }
}