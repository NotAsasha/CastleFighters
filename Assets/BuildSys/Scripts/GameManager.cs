using Assets.Build.Castle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

namespace Assets.Build.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static Dictionary<Vector2, Point> AllPoints = new();
        public Vector3 _position;
        private void Awake()
        {
            AllPoints.Clear();
            Time.timeScale = 0;
            Load();
        }
        private void Load()
        {
            string path = Application.persistentDataPath + "/Castles/";
            if (PlayerPrefs.GetInt("NewCastle") == 1)
            {
                CastleInstantiator.InstantiateCastle(LoadCastle.LoadNewCastle());
                GameObject _castle = GameObject.FindGameObjectWithTag("Castle");
                _castle.transform.position = _position;
                Debug.Log("LoadingNewCastle");
                return;
            }
            List<CastleData> castles = LoadCastle.LoadCastles(path);
            foreach (CastleData castle in castles)
            {
                if (castle.CastleName == PlayerPrefs.GetString("CurrentCastle"))
                {
                    Debug.Log("LoadingOldCastle");

                    CastleInstantiator.InstantiateCastle(castle);
                    GameObject _castle = GameObject.FindGameObjectWithTag("Castle");
                    _castle.transform.position = _position;
                    return;
                }
            }
        }
    }
}
