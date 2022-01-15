using UnityEngine;

public class DebugBehaviour : MonoBehaviour
{
    public void Log(string log)
    {
        Debug.Log(log);
    }
    public void LogWarning(string log)
    {
        Debug.LogWarning(log);
    }
    public void LogError(string log)
    {
        Debug.LogError(log);
    }
}
