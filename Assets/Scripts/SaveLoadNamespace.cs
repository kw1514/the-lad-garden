using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SaveLoadNamespace : MonoBehaviour
    {
        public void SaveNew()
        {
            SaveLoadSystem.SaveNew();
        }

        public void Save()
        {
            SaveLoadSystem.Save();
        }

        public void Load()
        {
            SaveLoadSystem.Load();
        }

        public void Delete()
        {
            SaveLoadSystem.Delete();
        }

        public string GetFullSavePath()
        {
            return SaveLoadSystem.fullSavePath;
        }

        public void SetSavePath(string path)
        {
            SaveLoadSystem.savePath = path;
        }

        public void SetSaveName(string name)
        {
            SaveLoadSystem.saveName = name;
        }
    }
}
