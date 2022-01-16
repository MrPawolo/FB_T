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
            if (scenesHolder == null)
            {
                Debug.LogError("No Scene Holder is Attached", this.gameObject);
                return;
            }
            var scenes = scenesHolder.GetSceneIndexes();
           
            foreach (var scene in scenes)
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }

    }
}


