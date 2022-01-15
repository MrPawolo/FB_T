#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    [SerializeField] SceneAsset[] scenesToLoad;

    
    public SceneAsset[] ScenesToLoad {  get { return scenesToLoad; } }


}
 
[CustomEditor(typeof(LoadScenes))]
public class LoadScenesEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        LoadScenes loadScenes = (LoadScenes)target;

        if(GUILayout.Button("Load Scenes"))
        {
            if (loadScenes.ScenesToLoad.Length == 0)
                return;
            for (int i = 0; i < loadScenes.ScenesToLoad.Length; i++) 
            {
                string scenePath = AssetDatabase.GetAssetPath(loadScenes.ScenesToLoad[i]);
                string buildName = scenePath.Replace("Assets\"", string.Empty);
                if (SceneUtility.GetBuildIndexByScenePath(scenePath) < 0)
                {
                    Debug.Log($"Scene with name \"{loadScenes.ScenesToLoad[i].name}\" is not in the build");
                    continue;
                }
                EditorSceneManager.OpenScene(buildName, OpenSceneMode.Additive);
            }
        }
        if (GUILayout.Button("Unload Scenes"))
        {
            if (loadScenes.ScenesToLoad.Length == 0)
                return;
            for (int i = 0; i < loadScenes.ScenesToLoad.Length; i++)
            {
                string scenePath = AssetDatabase.GetAssetPath(loadScenes.ScenesToLoad[i]);
                if (SceneUtility.GetBuildIndexByScenePath(scenePath) < 0)
                {
                    Debug.Log($"Scene with name \"{loadScenes.ScenesToLoad[i].name}\" is not in the build");
                    continue;
                }
                EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByName(loadScenes.ScenesToLoad[i].name), true);
            }
        }
    }

}

#endif
