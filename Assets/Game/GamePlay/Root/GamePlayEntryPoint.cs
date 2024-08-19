using UnityEngine;

public class GamePlayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _sceneRootBinder;


    public void Run()
    {
        Debug.Log("GamePlay Scene is Loaded!");
    }
}
