using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Assets.Build.Castle
{
    [System.Serializable]
    public class LoadCastle
    {
        public static List<CastleData> LoadCastles(string path)
        {
            var CastleList = new List<CastleData>();
            var dir = new DirectoryInfo(path);
            var info = dir.GetFiles("*.NotA");
            foreach (var f in info)
            {
                Debug.Log("Loaded: " + f.Name);
                var currentCastle = JsonUtility.FromJson<CastleData>(f.OpenText().ReadToEnd());
                CastleList.Add(currentCastle);
            }
            return CastleList;
        }   
        public static CastleData LoadNewCastle()
        {
            string _castlePreFab = Resources.Load<TextAsset>("Castle/Default").text;
            Debug.Log("Loaded: " + _castlePreFab);
            CastleData newCastle = JsonUtility.FromJson<CastleData>(_castlePreFab);
            return newCastle;
        }
    }
}
