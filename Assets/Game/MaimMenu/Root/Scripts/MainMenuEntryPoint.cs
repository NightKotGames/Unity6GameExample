using System;
using EntryPoint;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGamePlayRootBinder _sceneUIRootPrefab;

    public event Action GotoManiMenuSceneRequested = delegate { };

    internal void Run(UIRootView uIRoot)
    {
        Debug.Log("GamePlay Scene is Loaded!");

        var uiScene = Instantiate(_sceneUIRootPrefab);
        uIRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.OnGotoManiMenuButtonCkicked += () => GotoManiMenuSceneRequested.Invoke();

    }
}