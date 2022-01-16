using System;
using UnityEngine;
using ML.Pool;

public class ObjToPool : MonoBehaviour, IPoolable
{
    [SerializeField] float speed = 2f;
    [SerializeField] float lifetime = 1f;
    Action<GameObject> onRelease;


    float timeStart;

    Action<GameObject> IPoolable.onRelease { get; set; }

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        timeStart = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        if (timeStart + lifetime < Time.realtimeSinceStartup)
        {
            onRelease(this.gameObject);
        }
        transform.position = transform.position + Vector3.right * speed;
    }
}
