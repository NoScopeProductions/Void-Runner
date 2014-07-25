using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFade : MonoBehaviour {

    public float FadeTime;
    public void Start()
    {
        FadeIn();
    }

    public void FadeOut()
    {
        gameObject.SetActive(true);
        iTween.FadeTo(gameObject, 1f, FadeTime);
    }

    public void FadeIn()
    {
        iTween.FadeTo(gameObject, iTween.Hash("time", FadeTime, "amount", 0f, "oncomplete", "ToggleEnabled", "oncompleteparams", gameObject));
    }

    private void ToggleEnabled()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}
