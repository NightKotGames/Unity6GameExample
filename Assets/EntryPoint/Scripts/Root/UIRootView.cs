
using UnityEngine;

namespace EntryPoint
{
    internal class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Transform _uiSceneContainer;

        private void Awake() => HideLoadingScreen(); 
        internal void ShowLoadingScreen() => _loadingScreen.SetActive(true);        
        internal void HideLoadingScreen() => _loadingScreen.SetActive(false);

        internal void AttachSceneUI(GameObject sceneUI)
        {
            ClearSceneUI();
            sceneUI.transform.SetParent(_uiSceneContainer, false);
        }

        private void ClearSceneUI()
        {
            while (_uiSceneContainer.childCount > 0)
                DestroyImmediate(_uiSceneContainer.GetChild(0).gameObject);
        }
    }
}
