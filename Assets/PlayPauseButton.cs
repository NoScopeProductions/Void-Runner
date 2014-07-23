using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayPauseButton : MonoBehaviour
{
    public Texture2D PlayButton;
    public Texture2D PauseButton;

    public GameObject SoundMenu;

    private bool Paused = false;

    void OnMouseUp()
    {
        Paused = !Paused;

        gameObject.guiTexture.texture = Paused ? PlayButton : PauseButton;

        if (Paused)
        {
            Time.timeScale = 0f;
            SoundMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = PlayerPreferences.CurrentTimeScale;
            SoundMenu.SetActive(false);
        }
    }
}
