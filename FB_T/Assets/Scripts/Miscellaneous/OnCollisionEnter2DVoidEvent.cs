using ML.GameEvents;
using UnityEngine;

public class OnCollisionEnter2DVoidEvent : MonoBehaviour
{
    [SerializeField] VoidEvent voidEvent;
    [SerializeField] LayerMask layerMask;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(HelperFunctions.IsOnLayer(layerMask, collision.gameObject.layer))
            voidEvent?.Invoke();
    }
}
