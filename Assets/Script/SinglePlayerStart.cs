using Assets.Build.Castle;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerStart : MonoBehaviour
{
    public Vector3 _position;
    public string _castleName;
    private Castle _castle;
    public void StartGame()
    {
        Load();
        _castle.transform.localScale = new Vector3(3, 3, 3);

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
        Time.timeScale = 1;
    }
    private void Load()
    {
        string path = Application.persistentDataPath + "/Castles/";
        List<CastleData> castles = LoadCastle.LoadCastles(path);

        foreach (CastleData castle in castles)
        {
            if (castle.CastleName == _castleName)
            {
                Debug.Log("LoadingOldCastle");

                CastleInstantiator.InstantiateCastle(castle);
                _castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle>();
                _castle.transform.position = _position;
                return;
            }
        }
    }
}
