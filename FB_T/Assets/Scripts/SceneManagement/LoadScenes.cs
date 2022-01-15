using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ML.SceneManagement
{
    public class LoadScenes : MonoBehaviour
    {
        [SerializeField] ScenesHolder scenesHolder;
        public ScenesHolder ScenesHolder { get { return scenesHolder; } }

        private void Start()
        {
            var scenes = scenesHolder.GetSceneIndexes();
            foreach(var scene in scenes)
            {
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                asyncOperation.allowSceneActivation = true;

            }
        }
    }
}


