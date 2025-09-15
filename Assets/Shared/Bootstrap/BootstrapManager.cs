using UnityEngine;

namespace Asce.Managers.Bootstrap
{
    /// <summary>
    ///     Manages the bootstrap process of the application.
    /// </summary>
    public class BootstrapManager : MonoBehaviourSingleton<BootstrapManager>
    {
        [SerializeField] private string _sceneToLoad = "Menu";
        [SerializeField] private float _delay = 0f;

        private void Start()
        {
            SceneLoader.Instance.Load(_sceneToLoad, isShowLoadingScene: true, delay: _delay);
        }
    }

}