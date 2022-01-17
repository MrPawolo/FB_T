using UnityEngine;
using ML.GameEvents;

public class OnTriggerEnder2DVoidEvent : MonoBehaviour
{
    [SerializeField] VoidEvent voidEvent;
    [SerializeField] LayerMask layerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HelperFunctions.IsOnLayer(layerMask, collision.gameObject.layer))
            voidEvent?.Invoke();
    }
}
