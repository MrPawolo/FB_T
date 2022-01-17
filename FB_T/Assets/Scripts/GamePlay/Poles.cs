using UnityEngine;

namespace ML.GamePlay
{
    public class Poles : MonoBehaviour
    {
        [SerializeField] GamePlaySettings gamePlaySettings;

        Transform[] childrens;
        Vector3[] startPos;

        private void Awake()
        {
            int childCount = transform.childCount;
            childrens = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                childrens[i] = transform.GetChild(i);
            }
            startPos = new Vector3[childrens.Length];
            for (int i = 0; i < startPos.Length; i++)
            {
                startPos[i] = childrens[i].localPosition;
            }
        }
        private void OnEnable()
        {
            for (int i = 0; i < childrens.Length; i++)
            {
                childrens[i].localPosition = new Vector3(transform.localPosition.x, startPos[i].y + (Mathf.Sign(startPos[i].y) * (gamePlaySettings.GapSize / 2)), transform.localPosition.z);
            }
        }
    }
}
