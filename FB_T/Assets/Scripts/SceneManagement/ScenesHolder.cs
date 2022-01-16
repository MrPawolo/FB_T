using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace ML.SceneManagement
{
    [CreateAssetMenu(fileName = "New Scenes Holder", menuName = "ML/SceneManagement/Scene Holder")]
    public class ScenesHolder : ScriptableObject
    {
        [SerializeField] List<string> scenesIndexes = new List<string>();
        public List<string> GetSceneIndexes()
        {
            return scenesIndexes;
        }


#if UNITY_EDITOR

        [SerializeField] private SceneAsset[] sceneAssets;

        private void OnValidate()
        {
            BuildScenes();
        }
        void BuildScenes()
        {
            scenesIndexes.Clear();
            for (int i = 0; i < sceneAssets.Length; i++)
            {
                if (sceneAssets[i] == null)
                    continue;
                string scenePath = AssetDatabase.GetAssetPath(sceneAssets[i]);
                string buildName = scenePath.Replace("Assets/", string.Empty).Replace(".unity",string.Empty);
                if (!scenesIndexes.Contains(buildName))
                {
                    scenesIndexes.Add(buildName);
                }
            }
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
