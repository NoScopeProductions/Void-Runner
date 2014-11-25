using UnityEngine;
using System.Collections;
using GameState = GlobalPreferences.GameState;

public class BackKeyListener : MonoBehaviour 
{
    public dfPanel QuitPanel;
    public bool ShowQuitPanel = false;

	void Awake()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{		 
		    if (ShowQuitPanel)
		    {
                QuitPanel.Show();   
		    }
		    else
		    {
		        QuitPanel.Hide();
		    }
            ShowQuitPanel = !ShowQuitPanel;
		}
	}

	void PauseGame()
	{
		GlobalPreferences.currentState = GameState.PAUSED;
	}

	void LoadMenu ()
	{
		GlobalPreferences.SetDefaultTimeScale ();
		Application.LoadLevel ("Menu");
		GlobalPreferences.currentState = GameState.MAIN_MENU;
	}
}
