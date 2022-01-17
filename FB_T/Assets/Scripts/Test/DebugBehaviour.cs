using UnityEngine;

public class DebugBehaviour : MonoBehaviour
{
    public void Log(float log)
    {
        Debug.Log(log);
    }
    public void LogWarning(float log)
    {
        Debug.LogWarning(log);
    }
    public void LogError(float log)
    {
        Debug.LogError(log);
    }

    public void Log(int log)
    {
        Debug.Log(log);
    }
    public void LogWarning(int log)
    {
        Debug.LogWarning(log);
    }
    public void LogError(int log)
    {
        Debug.LogError(log);
    }


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
