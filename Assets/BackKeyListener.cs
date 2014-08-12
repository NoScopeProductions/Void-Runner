using UnityEngine;
using System.Collections;
using GameState = GlobalPreferences.GameState;

public class BackKeyListener : MonoBehaviour 
{
	public GameObject selectionBackButton;
	public GameObject statsBackButton;
	public GameObject creditsBackButton;
	public GameObject pauseButton;

	void OnLevelWasLoaded(int scene) 
	{
		switch (scene)
		{
			case 1:
				GlobalPreferences.currentState = GameState.MAIN_MENU;
				break;

			case 2:
				GlobalPreferences.currentState = GameState.PLAYING;
				break;
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			switch(GlobalPreferences.currentState)
			{
				case GameState.MAIN_MENU: 
				{
					Application.Quit();
					break;
				}
				case GameState.SELECTION: 
				{
					BackFromSelectionMenu();
					break;
				}
				case GameState.PLAYING: 
				{
					PauseGame();
					break;
				}
				case GameState.PAUSED:
				case GameState.GAME_OVER:  
				{
					LoadMenu ();
					break;
				}
				case GameState.CREDITS: 
				{
					BackFromCreditsMenu();
					break;
				}
				case GameState.STATS:
				{
					BackFromStats();
					break;
				}
			}
		}
	}

	void BackFromSelectionMenu ()
	{
		if(!selectionBackButton)
		{
			return;
		}

		NavigationButton button = selectionBackButton.GetComponent<NavigationButton>();
		button.Activate ();

		GlobalPreferences.currentState = GameState.MAIN_MENU;
	}

	void BackFromCreditsMenu()
	{
		if(!creditsBackButton)
		{
			return;
		}

		BackFromCredits button = creditsBackButton.GetComponent<BackFromCredits> ();
		button.Activate ();

		GlobalPreferences.currentState = GameState.MAIN_MENU;
	}

	void BackFromStats()
	{
		if(!statsBackButton)
		{
			return;
		}

		NavigationButton button = statsBackButton.GetComponent<NavigationButton>();
		button.Activate ();

		GlobalPreferences.currentState = GameState.MAIN_MENU;
	}

	void PauseGame()
	{
		if(!pauseButton)
		{
			return;
		}

		PlayPauseButton button = pauseButton.GetComponent<PlayPauseButton> ();
		button.TogglePauseAndTexture ();

		GlobalPreferences.currentState = GameState.PAUSED;
	}

	void LoadMenu ()
	{
		Application.LoadLevel ("Menu");
		GlobalPreferences.currentState = GameState.MAIN_MENU;
	}
}
