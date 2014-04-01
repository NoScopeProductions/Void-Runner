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

		if(GUILayout.Button ("Left")) 
		{
			PlayerPreferences.shipSelected = 1;
			Application.LoadLevel("Game");
		}
		if(GUILayout.Button ("Middle"))
		{
			PlayerPreferences.shipSelected = 2;
			Application.LoadLevel("Game");
		}

		if(GUILayout.Button ("Right"))
		{
			PlayerPreferences.shipSelected = 3;
			Application.LoadLevel("Game");
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}
}
