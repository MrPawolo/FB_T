using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.GameEvents;

public class Poles : MonoBehaviour
{
    [SerializeField] GamePlaySettings gamePlaySettings;

    Transform[] childrens;
    Vector3[] startPos;

    private void Awake()
    {
        childrens = GetComponentsInChildren<Transform>();
        startPos = new Vector3[childrens.Length];
        for(int i = 0; i< startPos.Length; i++)
        {
            startPos[i] = childrens[i].position;
        }
    }
    private void OnEnable()
    {
        for(int i = 0; i < childrens.Length; i++)
        {
            childrens[i].position = new Vector3(startPos[i].x, startPos[i].y + (Mathf.Sign(startPos[i].y) * (gamePlaySettings.GapSize / 2)), startPos[i].z);
        }
    }

}
