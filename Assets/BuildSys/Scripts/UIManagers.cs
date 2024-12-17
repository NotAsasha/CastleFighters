using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Build.Castle;

namespace Assets.Build.Scripts
{
    public class UIManagers : MonoBehaviour
    {
        public Vector3 castlePosition;
        public Button RoadButton;
        public Button WoodenButton;
        public Button GlassButton;
        public Button DoorButton;
        public BarCreator barCreator;

        public GameObject BuildCamera;
        public GameObject PlayCamera;
        public GameObject ViewBothPoint;

        public SaveCastle _saveCastle;

        private void Start()
        {
            RoadButton.onClick.Invoke();
        }


        public void Play()
        {
            Castle.Castle _castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle.Castle>();

            if (barCreator.BarCreationStarted)
            {
                barCreator.BarCreationStarted = false;
                barCreator.DeleteCurrentBar();
                Debug.Log("DestroyedBar");
                return;
            }

            _castle.transform.position = castlePosition;
            if (_saveCastle.NameInput.text.Length < 3)
            {
                _saveCastle.NameInput.text = "Untitled";
            }
            string path = Application.persistentDataPath + "/Castles/" + _saveCastle.NameInput.text + ".NotA";

            if (!Directory.Exists(Application.persistentDataPath + "/Castles"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Castles");
            int failedAttempts = 0;
            while (File.Exists(path))
            {
                failedAttempts += 1;
                _saveCastle.NameInput.text = "Untitled" + failedAttempts.ToString();
                path = Application.persistentDataPath + "/Castles/" + _saveCastle.NameInput.text + ".NotA";
            }
            _saveCastle.SaveCastleAsPrefab();
            PlayerPrefs.SetString("CurrentCastle", _saveCastle.NameInput.text);


            _castle.transform.localScale = new Vector3(3,3,3);
            BuildCamera.SetActive(false);
            PlayCamera.SetActive(true);


            int pointCounter = 0;
            foreach (Transform child in _castle.PointParent.transform)
            {
                pointCounter++;
                if (pointCounter > 2)
                    child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            }
            foreach (Transform child in _castle.BarParent.transform)
            {
                child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            Time.timeScale = 1f;
        }


        public void Restart()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void ChangeBar(int myBarType)
        {
            WoodenButton.GetComponent<Outline>().enabled = false;
            RoadButton.GetComponent<Outline>().enabled = false;
            GlassButton.GetComponent<Outline>().enabled = false;
            DoorButton.GetComponent<Outline>().enabled = false;
            switch (myBarType)
            {
                case 0:
                    RoadButton.GetComponent<Outline>().enabled = true;
                    barCreator.BarToInstantiate = barCreator.RoadBar;
                    break;
                case 1:
                    WoodenButton.GetComponent<Outline>().enabled = true;
                    barCreator.BarToInstantiate = barCreator.WoodenBar;
                    break;
                case 2:
                    GlassButton.GetComponent<Outline>().enabled = true;
                    barCreator.BarToInstantiate = barCreator.GlassBar;
                    break;
                case 3:
                    DoorButton.GetComponent<Outline>().enabled = true;
                    barCreator.BarToInstantiate = barCreator.DoorBar;
                    break;
            }
        }
    }
}
