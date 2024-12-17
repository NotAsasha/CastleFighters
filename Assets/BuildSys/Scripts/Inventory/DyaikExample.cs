using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyaikExample : MonoBehaviour
{

    private string DyakName = "Dyak";
    private int DyakCount = 1;

    void Start()
    {
        AddDyak("Andriy", 1);
        AddDyak("Vrodi", 3);
    }

    private void AddDyak(string howmachDyakToAdd, int DyakToAdd)
    {
        DyakName += howmachDyakToAdd;
        DyakCount += DyakToAdd;
        Debug.Log(DyakName + ", " + DyakCount);
    }

    // Dyak = DyakAndriy, 2
    // Dyak = DyakAndriyVrodi, 5
}
