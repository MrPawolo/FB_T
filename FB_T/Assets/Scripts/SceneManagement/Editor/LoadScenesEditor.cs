
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace ML.SceneManagement
{
    [CustomEditor(typeof(LoadScenes))]
    public class LoadScenesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LoadScenes loadScenes = (LoadScenes)target;

            if (loadScenes == null)
                return;
            if (loadScenes.ScenesHolder == null)
                return;
            if (GUILayout.Button("Load Scenes"))
            {
                var scenes = loadScenes.ScenesHolder.GetSceneIndexes();
                foreach(var scene in scenes)
                {
                    EditorSceneManager.OpenScene("Assets/" + scene + ".unity", OpenSceneMode.Additive);
                }
            }
            if (GUILayout.Button("Unload Scenes"))
            {
                var scenes = loadScenes.ScenesHolder.GetSceneIndexes();
                foreach (var scene in scenes)
                {
                    string[] names = scene.Split('/');
                    int lastIndex = names.Length - 1 < 0 ? 0 : names.Length - 1;
                    Scene scene1 = EditorSceneManager.GetSceneByName(names[lastIndex]);
                    EditorSceneManager.CloseScene(scene1, true);
                }
            }
        }

    }
}


