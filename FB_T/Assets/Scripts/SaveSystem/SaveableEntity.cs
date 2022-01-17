using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ML.SaveSystem
{
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string id = string.Empty;

        public string Id => id;

        [ContextMenu("Generate Id")]
        void GenerateId() => id = Guid.NewGuid().ToString();

        private void OnValidate()
        {
            if(id.Equals(string.Empty))
            {
                GenerateId();
            }
        }

        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
                data[saveable.GetType().ToString()] = saveable.CaptureState();
            }

            return data;
        }

        public void RestoreData(object data)
        {
            Dictionary<string, object> loadedData = (Dictionary<string, object>)data;

            foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
                string objectType = saveable.GetType().ToString();

                if(loadedData.TryGetValue(objectType, out object state))
                {
                    saveable.RestoreState(state);
                }
            }
        }
    }
}
