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

    void OnMouseUp()
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
}
