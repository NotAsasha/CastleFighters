using Assets.Build.Castle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeleteFile : MonoBehaviour
{
    [SerializeField] private Text Text;
    string path;


    public void DeleteCastleFile()
    {
        string name = Text.GetComponentInChildren<Text>().text;
        path = Application.persistentDataPath + "/Castles/" + name + ".NotA";
        Debug.Log(path);
        
        System.IO.File.Delete(path);
        GetComponentInParent<CastleMenu>().ReloadCastles(true);
    }
}
