using UnityEngine;
using System.Collections;

public class NavigationButton : MonoBehaviour 
{
	public GameObject FadeTo;
	public GameObject FadeFrom;
	
	public Texture2D ButtonDown;
	public Texture2D ButtonNormal;
	
	void Activate()
	{
		FadeTo.SetActive(true);
		iTween.FadeTo(FadeTo, 1f, 0.4f);
		
		iTween.FadeTo(FadeFrom, iTween.Hash("time", 0.3f, "alpha", 0f, "oncomplete", "HideFrom", "oncompletetarget", gameObject));
	}
	
	private void HideFrom()
	{
		FadeFrom.SetActive(false);
	}
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
					guiTexture.texture = ButtonDown;
					break;
				case TouchPhase.Ended: //OnMouseUp
					guiTexture.texture = ButtonNormal;
					Activate();
					break;
				}
			}
		}
	}
	
	#if UNITY_EDITOR
	public void OnMouseUp()
	{
		guiTexture.texture = ButtonNormal;
		Activate();
	}
	
	public void OnMouseDown()
	{
		guiTexture.texture = ButtonDown;
	}
	#endif
}
