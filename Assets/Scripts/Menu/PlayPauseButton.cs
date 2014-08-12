using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayPauseButton : MonoBehaviour
{
    public Texture2D PlayButton;
    public Texture2D PauseButton;

    public GameObject SoundMenu;

    private bool Paused = false;

	void SetTimeScaleToZeroAndEnableSoundMenu()
	{
		Time.timeScale = 0f;
		SoundMenu.SetActive(true);
	}

	void SetTimeScaleToCurrentAndDisableSoundMenu()
	{
		Time.timeScale = GlobalPreferences.CurrentTimeScale;
		SoundMenu.SetActive(false);
	}

	public void TogglePauseAndTexture()
	{
		Paused = !Paused;
		gameObject.guiTexture.texture = Paused ? PlayButton : PauseButton;
	}

#if UNITY_EDITOR
    void OnMouseUp()
    {
		TogglePauseAndTexture();

        if (Paused)
        {
			SetTimeScaleToZeroAndEnableSoundMenu();
        }
        else
        {
			SetTimeScaleToCurrentAndDisableSoundMenu();
        }
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
				case TouchPhase.Ended: //OnMouseUp
					TogglePauseAndTexture();

					if (Paused)
					{
						SetTimeScaleToZeroAndEnableSoundMenu();
					}
					else
					{
						SetTimeScaleToCurrentAndDisableSoundMenu();
					}
					break;
				}
			}
		}
	}
#endif
}
