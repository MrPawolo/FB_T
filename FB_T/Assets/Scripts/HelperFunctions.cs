using UnityEngine;

public static class HelperFunctions
{
    public static bool IsOnLayer(LayerMask layerMask, int objectLayer)
    {
        return (layerMask.value & (1 << objectLayer)) > 0;
    }
}
