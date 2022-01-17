using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ML.GameEvents;

namespace ML.SceneManagement
{
    public class HideCurtain : MonoBehaviour
    {

        [SerializeField] RectTransform hidePanel;
        public void StartProcedure()
        {
            StartCoroutine(Hide());
        }
        public IEnumerator Hide()
        {
            float time = 1;
            while (time > 0)
            {
                hidePanel.anchorMax = new Vector2(1, Mathf.SmoothStep(0, 1, time));
                time -= Time.deltaTime;
                yield return null;
            }
            hidePanel.anchorMax = new Vector2(1, 0);
        }
        
    }
}
