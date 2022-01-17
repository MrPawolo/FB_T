using ML.GameEvents;
using UnityEngine;

public class OnTriggerExit2DVoidEvent : MonoBehaviour
{
    [SerializeField] VoidEvent voidEvent;
    [SerializeField] LayerMask layerMask;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (HelperFunctions.IsOnLayer(layerMask, collision.gameObject.layer))
            voidEvent?.Invoke();
    }
}
