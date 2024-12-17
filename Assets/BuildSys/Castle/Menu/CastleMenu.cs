using Assets.Build.Castle;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Build.Castle
{
    public class CastleMenu : MonoBehaviour
    {
        public GameObject _button = null;

        [SerializeField] public GameObject ButtonParent; 

        [SerializeField] private bool ifLoadLoadFolderCastles;
        [SerializeField] private bool ifLoadResoursesCastles;
        [SerializeField] private bool ifLoadLoadFolderLevels;

        private void Start()
        {
            if (ifLoadLoadFolderCastles) LoadFolderCastles();
            if (ifLoadResoursesCastles) LoadResoursesCastles();
            if (ifLoadLoadFolderLevels) LoadFolderLevels();
        }
        public void LoadFolderCastles()
        {
            var path = Application.persistentDataPath + "/Castles/";
            List<CastleData> CastleList = LoadCastle.LoadCastles(path);
            
            foreach (var castle in CastleList)
            {
                InstantiateButtons(castle);
            }
        }
        public void LoadResoursesCastles()
        {
            TextAsset[] CastleStrings = Resources.LoadAll<TextAsset>("Castle/Single/");
            foreach (var castle in CastleStrings)
            {
                CastleData castleData = JsonUtility.FromJson<CastleData>(castle.text);
                InstantiateButtons(castleData);
            }
        }
        public void LoadFolderLevels()
        {
            var path = Application.persistentDataPath + "/Levels/";
            List<CastleData> LevelList = LoadCastle.LoadCastles(path);
            foreach (var castle in LevelList)
            {
                InstantiateButtons(castle);
            }
        }
        public void InstantiateButtons(CastleData Castle)
        {
            if (!_button) _button = Resources.Load<GameObject>("Castle/CastleButton");
            Button castleButton = Instantiate(_button, ButtonParent.transform).GetComponent<Button>();
            castleButton.GetComponentInChildren<Text>().text = Castle.CastleName;
            castleButton.GetComponent<CastleButton>().CastleName = Castle.CastleName;
        }
        public void ReloadCastles(bool withIcons)
        {
            if (withIcons)
            {
                foreach(Transform child in ButtonParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            LoadFolderCastles();
        }
    }
}

