using UnityEngine;
using System.Collections;

public class BackButtonFromStats : MonoBehaviour 
{
	public GameObject StatisticsMenu;
	public GameObject MainMenu;
	
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
					//guiTexture.texture = PlayButton_Down;
					break;
				case TouchPhase.Ended: //OnMouseUp
					//guiTexture.texture = PlayButton_Normal;
					//Activate();
					break;
				}
			}
		}
	}
}
