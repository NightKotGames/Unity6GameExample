
using EntryPoint;
using R3;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    internal Observable<MainMenuExitParams> Run(UIRootView uIRoot, MainMenuEnterParams enterParams)
    {
        Debug.Log("GamePlay Scene is Loaded!");

        var uiScene = Instantiate(_sceneUIRootPrefab);
        uIRoot.AttachSceneUI(uiScene.gameObject);

        // Создаем Сабж
        var exitSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSignalSubj);

        Debug.Log($"MAIN MENU ENTRY POINT: Run Main Menu Scene - Result: {enterParams?.Result}");

        // Добавляем параметры 
        var saveFileName = $"{Application.productName}.save";
        var levelNumber = UnityEngine.Random.Range(1, 100);
        var gamePlayEnterParams = new GamePlayEnterParams(saveFileName, levelNumber);
        var mainMenuExitParams = new MainMenuExitParams(gamePlayEnterParams);

        // Преобразуем Сигнал в класс выходной Пврвметр.
        var exitToGamePlaySceneSignal = exitSignalSubj.Select(_ => mainMenuExitParams);

       return exitToGamePlaySceneSignal;
    }
}