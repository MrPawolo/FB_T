using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ML.SaveSystem {
    public class SavingLoading : MonoBehaviour
    {
        string SavePath => $"{Application.persistentDataPath}/save.save";

        Dictionary<string, object> state;

        public static SavingLoading Instance { get; private set; }
        static bool instancieted = false;

        private void Awake()
        {
            if (instancieted)
                Destroy(this.gameObject);

            Instance = this;
            DontDestroyOnLoad(gameObject);
            instancieted = true;
        }

        public object GetDataAfterLoad(string id)
        {
            if (state == null)
            {
                Debug.Log("File wasnt loaded yet");
                return null;
            }
            if(state.TryGetValue(id, out object value))
            {
                return value;
            }
            Debug.Log("No object was found");
            return null;
        }

        [ContextMenu("Save")]
        public void Save()
        {
            Dictionary<string, object> state = LoadFile(); //find existing data because We dont want to ovveride them
            CaptureState(state); //fill state with data 
            SaveFile(state); //save to file
            Debug.Log("Saved");
        }

        [ContextMenu("Load")]
        public void Load()
        {
            state = LoadFile();
            RestoreState(state);
            Debug.Log("Loaded");
        }
        void CaptureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }

        void RestoreState(Dictionary<string,object> state)
        {
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                if(state.TryGetValue(saveable.Id, out object value))
                {
                    saveable.RestoreData(value);
                }
            }
        }

        void SaveFile(object state)
        {
            using (FileStream stream = File.Open(SavePath, FileMode.Create))
            {
                BinaryFormatter formater = new BinaryFormatter();
                formater.Serialize(stream, state);
            }
        }

        Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(SavePath))
            {
                return new Dictionary<string, object>();
            }

            using(FileStream stream = File.Open(SavePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }
    } 
}
