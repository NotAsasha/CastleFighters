using Assets.Build.Castle;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class CastleButton : MonoBehaviour
{
    public string CastleName;
    public void LoadBuildMenu()
    {
        PlayerPrefs.SetInt("NewCastle", 0);
        PlayerPrefs.SetString("CurrentCastle", CastleName);
        SceneManager.LoadScene("BuildScene");
    }
    public void CreateNewCastle()
    {
        PlayerPrefs.SetInt("NewCastle", 1);
        SceneManager.LoadScene("BuildScene");
    }
    public void BuildCastle(bool isEnemy)
    {
        string path = Application.persistentDataPath + "/Castles/";
        List<CastleData> castles = LoadCastle.LoadCastles(path);
        foreach (CastleData castle in castles)
        {
            if (castle.CastleName == CastleName)
            {
                Debug.Log("LoadingOldCastle");
                GameObject _castle = CastleInstantiator.InstantiateCastle(castle);
                if (isEnemy) { _castle.transform.position = LocalSceneManager.instance.enemyCastlePosition;LocalSceneManager.instance.enemyCastle = _castle;}
                if (!isEnemy) {_castle.transform.position = LocalSceneManager.instance.yourCastlePosition; LocalSceneManager.instance.yourCastle = _castle;}
                _castle.transform.localScale = new Vector3(3, 3, 3);
                LocalSceneManager.instance.UpdateUI();
                return;
            }
        }
    }
}
