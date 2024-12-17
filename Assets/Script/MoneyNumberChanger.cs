using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MoneyNumberChanger : MonoBehaviour
{

    public string moneyToShow = "Smth isn't working";

    public Sprite coin1;
    public Sprite coin2;
    public Sprite coin3;
    public Sprite easterCoin;

    private Sprite spriteToChange;
    private Vector3 scaleToChange;
    private void Update()
    {
        int Money = MoneyCounter.Money();
        moneyToShow = string.Format("${0:#,.000}", Money);
        GetComponent<TMP_Text>().text = moneyToShow;
        switch (Money)
        {
            case var _ when (Money >=5000 && Money < 10001):
                scaleToChange = new Vector3(0.36f, 0.9f, 1);
                spriteToChange = coin1;
                break;
            case var _ when (Money >= 2500 && Money < 5000):
                spriteToChange = coin2;
                scaleToChange = new Vector3(0.36f, 0.7f, 1);
                break;
            case var _ when (Money != 1 && Money < 2500):
                spriteToChange = coin3;
                scaleToChange = new Vector3(0.36f, 0.7f, 1);
                break;
            case 1 :
                spriteToChange = easterCoin;
                scaleToChange = new Vector3(0.36f, 1f, 1);
                break;
        }
        foreach (Image image in GetComponentsInChildren<Image>())
        {
            image.sprite = spriteToChange;
            image.gameObject.transform.localScale = scaleToChange;

        }
    }
}
