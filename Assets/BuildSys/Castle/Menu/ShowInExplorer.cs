using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowInExplorer : MonoBehaviour
{
    public string path;

    public void OpenExplorer()
    {
        Application.OpenURL("file://[C:/Users/kanal/Mission Demolition/Assets/Castles/]");
        Debug.Log(Application.persistentDataPath + path);
    }
}
