using R3;
using DI;
using EntryPoint;
using UnityEngine;

public class GamePlayEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGamePlayRootBinder _sceneUIRootPrefab;

    internal Observable<GamePlayExitParams> Run(DIContainer container, GamePlayEnterParams enterParams)
    {
        /// Регистируем все сервисы и сущности для этой Сцены        
        GamePlayRegistrations.Register(container, enterParams);

        /// Создаем и регистрируем новый контейнер для ViewModel
        var gamePlayViewModelContainer = new DIContainer(container);
        GamePlayViewModelRegistration.Register(gamePlayViewModelContainer);

        ///
        /// Для Текста:
        gamePlayViewModelContainer.Resolve<UIGamePlayRootViewModel>();
        gamePlayViewModelContainer.Resolve<WorldGamePlayRootViewModel>();

        Debug.Log("GamePlay Scene is Loaded!");

        var uIRoot = container.Resolve<UIRootView>();
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uIRoot.AttachSceneUI(uiScene.gameObject);

        // Создаем Сабж выходных данных

        var exitSceneSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSceneSignalSubj);

        Debug.Log($"GAMEPLAY ENTRY POINT: saveFileName = {enterParams.SaveFileName}, Level to Load = {enterParams.LevelNumber}");

        // Создаем какие то выходные параметры Сцены

        var mainMenuEnterParams = new MainMenuEnterParams("Vvariable params for Main Menu from GamePlay Scene...");
        var exitParams = new GamePlayExitParams(mainMenuEnterParams);

        // Преобразовываем Unit (exitSceneSignalSubj) в Выходной Тип
        
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        return exitToMainMenuSceneSignal;
    }
}