using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour 
{
	private float screenWidth;
	private float screenHeight;
    private Color invisible = new Color(1, 1, 1, 0);

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

        GUI.color = invisible;

		GUILayout.BeginArea (new Rect(0, screenHeight/2 - screenHeight/10, screenWidth, screenHeight));
		GUILayout.BeginHorizontal ("");

		if(GUILayout.Button("Remaker", GUILayout.Height(screenHeight/4))) 
		{
            PlayerPreferences.shipSelected = PlayerPreferences.SHIP.REMAKER;
			Application.LoadLevel("Game");
		}
        if (GUILayout.Button("Default", GUILayout.Height(screenHeight / 4)))
		{
            PlayerPreferences.shipSelected = PlayerPreferences.SHIP.DEFAULT;
			Application.LoadLevel("Game");
		}

        if (GUILayout.Button("DSK", GUILayout.Height(screenHeight / 4)))
		{
            PlayerPreferences.shipSelected = PlayerPreferences.SHIP.DSK;
			Application.LoadLevel("Game");
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}
}
