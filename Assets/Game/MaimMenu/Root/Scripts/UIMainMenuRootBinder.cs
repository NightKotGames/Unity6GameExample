
using System;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    public event Action OnGotoGamePlaySceneButtonCkicked = delegate { };

    public void HandleGotoGamePlaySceneButtonClick() => OnGotoGamePlaySceneButtonCkicked.Invoke();
}