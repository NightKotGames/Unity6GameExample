
using R3;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    private Subject<Unit> _exitSignalSceneSubj;

    public void HandleGotoGamePlaySceneButtonClick() => _exitSignalSceneSubj.OnNext(Unit.Default);

    public void Bind(Subject<Unit> exitSceneSignalSubj) => _exitSignalSceneSubj = exitSceneSignalSubj;
}