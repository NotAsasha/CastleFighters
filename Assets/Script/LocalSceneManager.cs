using Assets.Build.Castle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSceneManager : MonoBehaviour
{
    public static LocalSceneManager instance;
    public GameObject yourCastle;
    public GameObject enemyCastle;
    public Vector3 yourCastlePosition;
    public Vector3 enemyCastlePosition;
    [SerializeField] GameObject yourUI;
    [SerializeField] GameObject enemyUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject gameUI;
    private void Start()
    {
        instance = this;
    }
    public void UpdateUI()
    {
        if (yourCastle) yourUI.SetActive(false);
        if (enemyCastle) enemyUI.SetActive(false);
        if (yourCastle && enemyCastle)
        {
            StartGame();

        }
    }
    public void StartGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        StartCastle(yourCastle.GetComponent<Castle>());
        StartCastle(enemyCastle.GetComponent<Castle>());

        Time.timeScale = 1;
    }
    private void StartCastle(Castle _castle)
    {
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
        
    }
}
