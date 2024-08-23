
using R3;
using DI;
using EntryPoint;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    internal Observable<MainMenuExitParams> Run(DIContainer container, MainMenuEnterParams enterParams)
    {
        /// ����������� ��� ������� � �������� ��� ���� �����        
        MainMenuRegistrations.Register(container, enterParams);

        /// ������� � ������������ ����� ��������� ��� ViewModel
        var mainMenuViewModelContainer = new DIContainer(container);
        MainMenuViewModelRegistration.Register(mainMenuViewModelContainer);

        ///
        /// ��� ������:
        mainMenuViewModelContainer.Resolve<UIMainMenuRootViewModel>();


        Debug.Log("GamePlay Scene is Loaded!");

        var uIRoot = container.Resolve<UIRootView>();
        var uiScene = Instantiate(_sceneUIRootPrefab);
        uIRoot.AttachSceneUI(uiScene.gameObject);

        // ������� ����
        var exitSignalSubj = new Subject<Unit>();
        uiScene.Bind(exitSignalSubj);

        Debug.Log($"MAIN MENU ENTRY POINT: Run Main Menu Scene - Result: {enterParams?.Result}");

        // ��������� ��������� 
        var saveFileName = $"{Application.productName}.save";
        var levelNumber = UnityEngine.Random.Range(1, 100);
        var gamePlayEnterParams = new GamePlayEnterParams(saveFileName, levelNumber);
        var mainMenuExitParams = new MainMenuExitParams(gamePlayEnterParams);

        // ����������� ������ � ����� �������� ��������.
        var exitToGamePlaySceneSignal = exitSignalSubj.Select(_ => mainMenuExitParams);

       return exitToGamePlaySceneSignal;
    }
}