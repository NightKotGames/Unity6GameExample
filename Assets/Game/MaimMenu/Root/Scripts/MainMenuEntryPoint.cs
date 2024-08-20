using System;
using EntryPoint;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

    public event Action GotoGamePlaySceneRequested = delegate { };

    internal void Run(UIRootView uIRoot)
    {
        Debug.Log("GamePlay Scene is Loaded!");

        var uiScene = Instantiate(_sceneUIRootPrefab);
        uIRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.OnGotoGamePlaySceneButtonCkicked += () => GotoGamePlaySceneRequested.Invoke();

    }
}