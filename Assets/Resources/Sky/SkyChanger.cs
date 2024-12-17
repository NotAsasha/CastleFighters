using Assets.Build.Castle;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class SkyChanger : MonoBehaviour
{
    [SerializeField] private bool isRandom;
    public Light LightSource;
    public GameObject TorchesParent;
    void Start()
    {
        ChooseRandomWeather();
    }
    public void ChooseRandomWeather()
    {
        int time = Random.Range(1, 3);
        switch (time)
        {
            case 1:
                List<Material> DayMaterials= new List<Material>();
                var day = Resources.LoadAll<Material>("Sky/Day");
                foreach (Material mat in day)
                {
                    DayMaterials.Add(mat);
                }
                RenderSettings.skybox = DayMaterials[Random.Range(1, DayMaterials.Count)];
                LightSource.intensity = 1.6f;
                TorchesParent.SetActive(false);
                break;
            case 2:
                List<Material> NightMaterials = new List<Material>();
                var night = Resources.LoadAll<Material>("Sky/Night");
                foreach (Material mat in night)
                {
                    NightMaterials.Add(mat);
                }
                RenderSettings.skybox = NightMaterials[Random.Range(1, NightMaterials.Count)];
                LightSource.intensity = 0.3f;
                TorchesParent.SetActive(true);
                break;
        }
    }
}

