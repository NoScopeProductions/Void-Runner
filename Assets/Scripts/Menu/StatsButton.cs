using UnityEngine;
using System.Collections;

public class StatsButton : MonoBehaviour 
{
	public GameObject MainMenu;
	public GameObject StatsMenu;
    public Texture2D StatsButton_Down;
    public Texture2D StatsButton_Normal;

	void DisableMainMenu()
	{
		iTween.FadeTo(MainMenu, iTween.Hash("time", 0.3f, "alpha", 0f, "oncomplete", "HideMainMenu", "oncompletetarget", gameObject));
	}

	private void HideMainMenu()
	{
		MainMenu.SetActive(false);
	}

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
					guiTexture.texture = StatsButton_Down;
					break;
				case TouchPhase.Ended: //OnMouseUp
					DisableMainMenu();
					guiTexture.texture = StatsButton_Normal;
					break;
				}
			}
		}
	}
#endif

#if UNITY_EDITOR
	public void OnMouseUp()
	{
		guiTexture.texture = StatsButton_Normal;
		DisableMainMenu();
	}
	
	public void OnMouseDown()
	{
		guiTexture.texture = StatsButton_Down;
	}
#endif
}
