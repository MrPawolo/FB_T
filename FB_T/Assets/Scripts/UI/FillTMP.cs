using UnityEngine;
using TMPro;

namespace ML.UI
{
    public class FillTMP : MonoBehaviour
    {
        TMP_Text text;
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }
        public void ToText(int val)
        {
            text.text = val.ToString();
        }
        public void ToText(float val)
        {
            text.text = val.ToString();
        }
        public void ToText(string val)
        {
            text.text = val.ToString();
        }
    }
}
