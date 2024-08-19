
using UnityEngine;

namespace EntryPoint
{
    internal class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;

        private void Awake() => HideLoadingScreen(); 
        internal void ShowLoadingScreen() => _loadingScreen.SetActive(true);        
        internal void HideLoadingScreen() => _loadingScreen.SetActive(false);
    }
}
