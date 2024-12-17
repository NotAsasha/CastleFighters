using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FakeCamera : MonoBehaviour
{
    public Camera faceCamera;
    public Material faceMaterial;

    private void Start()
    {
        if (faceCamera.targetTexture != null)
        {
            faceCamera.targetTexture.Release();
        }
        faceCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        faceMaterial.mainTexture = faceCamera.targetTexture;
    }
}
