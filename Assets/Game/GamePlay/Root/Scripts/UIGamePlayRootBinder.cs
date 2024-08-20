using System;
using UnityEngine;

public class UIGamePlayRootBinder : MonoBehaviour
{
    public event Action OnGotoMainMenuButtonCkicked = delegate { };
    public event Action OnGotoGamePlaySceneButtonClicked = delegate { };

    public void HandleGotoMainMenuButtonClick() => OnGotoMainMenuButtonCkicked.Invoke();
}