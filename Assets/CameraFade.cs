using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFade : MonoBehaviour {

    private float AlphaFadeValue = 1f;
    public Texture BlackTexture;

    public void OnGUI()
    {
        AlphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 5);

        GUI.color = new Color(0, 0, 0, AlphaFadeValue);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BlackTexture);
    }
}
