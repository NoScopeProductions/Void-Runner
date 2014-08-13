using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayPauseButton : MonoBehaviour
{
    public Texture2D PlayButton;
    public Texture2D PauseButton;

    public GameObject SoundMenu;

    private bool Paused = false;

	private void SetTimeScaleToZeroAndEnableSoundMenu()
	{
		Time.timeScale = 0f;
		SoundMenu.SetActive(true);
	}

	private void SetTimeScaleToCurrentAndDisableSoundMenu()
	{
		Time.timeScale = GlobalPreferences.CurrentTimeScale;
		SoundMenu.SetActive(false);
	}

	private void TogglePauseAndTexture()
	{
		Paused = !Paused;
		gameObject.guiTexture.texture = Paused ? PlayButton : PauseButton;
	}

	public void Activate ()
	{
		TogglePauseAndTexture ();
		if (Paused) {
			SetTimeScaleToZeroAndEnableSoundMenu ();
		}
		else {
			SetTimeScaleToCurrentAndDisableSoundMenu ();
		}
	}

#if UNITY_EDITOR
    void OnMouseUp()
    {
		Activate ();

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
					Activate();
					break;
				}
			}
		}
	}
#endif
}
