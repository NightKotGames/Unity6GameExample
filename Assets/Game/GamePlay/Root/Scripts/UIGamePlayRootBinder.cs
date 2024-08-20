
using R3;
using UnityEngine;

public class UIGamePlayRootBinder : MonoBehaviour
{
    private Subject<Unit> _exitSceneSignalSubj;

    // ������ Action.Invoke �� R3
    public void HandleGotoMainMenuButtonClick() => _exitSceneSignalSubj?.OnNext(Unit.Default);

    public void Bind(Subject<Unit> exitSceneSignalSubj) => _exitSceneSignalSubj = exitSceneSignalSubj;
}