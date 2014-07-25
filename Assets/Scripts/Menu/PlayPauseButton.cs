using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayPauseButton : MonoBehaviour
{
    public Texture2D PlayButton;
    public Texture2D PauseButton;

    public GameObject SoundMenu;

    private bool Paused = false;

#if UNITY_EDITOR
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
            Time.timeScale = GlobalPreferences.CurrentTimeScale;
            SoundMenu.SetActive(false);
        }
    }
#endif
}
