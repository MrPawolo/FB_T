using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.GameEvents;

public class BombArea : MonoBehaviour
{
    [SerializeField] VoidListener onDoubleClick;
    [SerializeField] LayerMask layerMask;
    BoxCollider2D bombCollider;
    void Start()
    {
        bombCollider = GetComponent<BoxCollider2D>();
        onDoubleClick.onGameEventInvoke += OnBomb;
        onDoubleClick.HookToGameEvent();
    }

    void OnBomb(Void arg)
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(transform.position, bombCollider.size* transform.localScale, 0, Vector2.zero, 0, layerMask);

        for(int i = 0; i < raycastHit2Ds.Length; i++)
        {
            if(raycastHit2Ds[i].collider.TryGetComponent(out IDestroy destroy))
            {
                destroy.Destroy();
            }
        }
    }
}
