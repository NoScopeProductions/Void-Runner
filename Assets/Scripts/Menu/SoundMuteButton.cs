using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundMuteButton : MonoBehaviour {

    public Texture2D SoundEnabled;
    public Texture2D SoundDisabled;

    void Start()
    {
        if (AudioListener.pause)
        {
            gameObject.guiTexture.texture = SoundDisabled;
        }
    }
    private void Toggle()
    {
        AudioListener.pause = !AudioListener.pause;
        AudioListener.volume = 1 - AudioListener.volume;


        if (AudioListener.pause)
        {
            gameObject.guiTexture.texture = SoundDisabled;
        }
        else
        {
            gameObject.guiTexture.texture = SoundEnabled;
        }
    }

#if UNITY_EDITOR
    void OnMouseUp()
    {
        Toggle();
    }
#endif

#if UNITY_ANDROID
    public void Update()
    {
        if (Input.touchCount <= 0) return;
        foreach (var touch in Input.touches)
        {
            if (guiTexture.HitTest(touch.position))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began: //OnMouseDown
                        break;
                    case TouchPhase.Stationary: //OnMouseEnter
                        //Switch to active guitexture here
                        // guiTexture = PlayButtonDown
                        break;
                    case TouchPhase.Ended: //OnMouseUp
                        Toggle();
                        break;
                }
            }
        }
    }
#endif
}
