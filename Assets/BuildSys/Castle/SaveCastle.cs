using System;
using System.IO;
using System.Collections.Generic;
using Assets.Build.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Build.Castle
{
    public class SaveCastle : MonoBehaviour
    {
        private Castle _castle;
        public InputField NameInput;
        public CastleData CastleInfo = new();

        private StreamWriter writer;
        public void SaveCastleAsPrefab()
        {
            _castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle>();

            // Change prefab name
            try
            {
                _castle.CastleName = NameInput.text;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
                return;
            }

            SaveToJson(Application.persistentDataPath + "/Castles/" + _castle.CastleName + ".NotA");
        }

        private void SaveToJson(string path)
        {
            List<BarStorage> _barStorages = new();
            List<PointStorage> _pointStorages = new();

            foreach (Transform child in _castle.BarParent.transform)
            {
                _barStorages.Add(child.GetComponent<Bar>().SaveBar());
            }
            foreach (Transform child in _castle.PointParent.transform)
            {
                _pointStorages.Add(child.GetComponent<Point>().SavePoint());
            }

            CastleInfo.BarArray = _barStorages.ToArray();
            CastleInfo.PointArray = _pointStorages.ToArray();
            CastleInfo.CastleName = _castle.CastleName;

            
            string data = JsonUtility.ToJson(CastleInfo, true);

            if (!Directory.Exists(Application.persistentDataPath + "/Castles"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Castles");


            if (File.Exists(path))
            {
                Debug.LogWarning("File already exists");
                return;
            }
            writer = new StreamWriter(path, true);
            writer.Write(data);
            writer.Close(); 
        }
    }
}