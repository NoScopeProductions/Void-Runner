using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour 
{
	float screenWidth;
	float screenHeight;

	// Use this for initialization
	void Start () 
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}

	void OnGUI()
	{
		DrawButtons ();
	}

	void DrawButtons ()
	{
		GUILayout.BeginArea (new Rect(0, screenHeight/2, screenWidth, screenHeight/4));
		GUILayout.BeginHorizontal ("");

		if(GUILayout.Button ("Default")) 
		{
			PlayerPreferences.shipSelected = PlayerPreferences.SHIP.DEFAULT;
			Application.LoadLevel("Game");
		}
		if(GUILayout.Button ("Remaker"))
		{
            PlayerPreferences.shipSelected = PlayerPreferences.SHIP.REMAKER;
			Application.LoadLevel("Game");
		}

		if(GUILayout.Button ("DSK"))
		{
            PlayerPreferences.shipSelected = PlayerPreferences.SHIP.DSK;
			Application.LoadLevel("Game");
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}
}
