using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asce.Managers
{
    public class SceneLoader : DontDestroyOnLoadSingleton<SceneLoader>
    {
        private static readonly string LoadingSceneName = "Loading";

        /// <summary>
        /// Raised when a scene loading progress updates.
        /// Value range [0,1].
        /// </summary>
        public event Action<float> OnLoadingProgress;

        /// <summary>
        /// Fire-and-forget load. Use when you don't need to await.
        /// </summary>
        public async void Load(string sceneName, bool isShowLoadingScene = true, float delay = 0f)
        {
            await LoadAsync(sceneName, isShowLoadingScene, delay);
        }

        /// <summary>
        /// Load a new scene and return a Task so caller can await.
        /// </summary>
        public async Task LoadAsync(string sceneName, bool isShowLoadingScene = true, float delay = 0f)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("[SceneLoader] Cannot load scene: name is null or empty.");
                return;
            }

            if (isShowLoadingScene)
            {
                AsyncOperation loadingOp = SceneManager.LoadSceneAsync(LoadingSceneName);
                while (!loadingOp.isDone)
                {
                    await Task.Yield();
                }
            }

            // Step 2: Start loading the target scene asynchronously
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            if (operation == null)
            {
                Debug.LogError($"[SceneLoader] Failed to load scene: {sceneName}");
                return;
            }

            operation.allowSceneActivation = false;

            // Step 3: Monitor progress until loading reaches 90%
            while (operation.progress < 0.9f)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                OnLoadingProgress?.Invoke(progress);

                await Task.Yield();
            }

            // Ensure 100% progress reported
            OnLoadingProgress?.Invoke(1f);

            // Step 4: Apply delay before switching scenes
            if (delay > 0f)
            {
                await Task.Delay(TimeSpan.FromSeconds(delay));
            }

            // Step 5: Allow scene activation
            operation.allowSceneActivation = true;

            // Wait until it actually finishes
            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}
