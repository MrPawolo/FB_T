using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace ML.SceneManagement
{
    public class LoadScenes : MonoBehaviour
    {
        [SerializeField] ScenesHolder scenesHolder;
        [SerializeField] UnityEvent onSceneLoaded;
        public ScenesHolder ScenesHolder { get { return scenesHolder; } }

        private IEnumerator Start()
        {
            var scenes = scenesHolder.GetSceneIndexes();

            foreach (var scene in scenes)
            {
                bool isLoaded = false;
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                asyncOperation.completed += (arg) => isLoaded = true;
                while (!isLoaded)
                {
                    yield return null;
                }
            }
            onSceneLoaded?.Invoke();
            
        }

    }
}


