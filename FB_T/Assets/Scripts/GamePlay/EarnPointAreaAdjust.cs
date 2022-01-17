using UnityEngine;

namespace ML.GamePlay
{
    public class EarnPointAreaAdjust : MonoBehaviour
    {
        [SerializeField] GamePlaySettings gamePlaySettings;
        private void OnEnable()
        {
            transform.localScale = new Vector3(transform.localScale.x, gamePlaySettings.GapSize, transform.localScale.z);
        }
    }
}