using System;
using UnityEngine;

public class UIGamePlayRootBinder : MonoBehaviour
{
    public event Action OnGotoManiMenuButtonCkicked = delegate { };

    public void HandleGotoMainMenuButtonClick() => OnGotoManiMenuButtonCkicked.Invoke();

}