using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeAdjust : MonoBehaviour
{
    private void Awake()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height ;
        float cameraSize = (Camera.main.orthographicSize * 0.5f) / screenRatio;
        Camera.main.orthographicSize = cameraSize;
    }
}
