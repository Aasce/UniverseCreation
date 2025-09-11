using Asce.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Shared
{
    public class LoadingSceneManager : MonoBehaviourSingleton<LoadingSceneManager>
    {
        [SerializeField] private Slider _loadSlider;
        public Slider LoadSlider => _loadSlider;

        private void OnEnable()
        {
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.OnLoadingProgress += UpdateProgress;
            }
        }

        private void OnDisable()
        {
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.OnLoadingProgress -= UpdateProgress;
            }
        }

        private void UpdateProgress(float progress)
        {
            if (_loadSlider != null)
            {
                _loadSlider.value = progress;
            }
        }
    }
}
