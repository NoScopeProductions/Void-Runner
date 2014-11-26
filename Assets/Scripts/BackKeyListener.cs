using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameState = GlobalPreferences.GameState;

public class BackKeyListener : MonoBehaviour 
{
    public dfPanel QuitPanel;
    public bool QuitPanelShowing = false;
    public List<dfPanel> panelsToDisable;
    public 

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            QuitPanelShowing = !QuitPanelShowing;
		    if (QuitPanelShowing)
		    {
                ShowQuitPanel();
		    }
		    else
		    {
                HideQuitPanel();
		    }
		}
	}

    public void HideQuitPanel()
    {
        QuitPanel.Hide();
        panelsToDisable.ForEach(panel =>
        {
            var children = panel.GetComponentsInChildren<dfControl>();

            foreach (var child in children)
            {
                child.IsInteractive = true;
            }
        });
        if (Application.loadedLevelName.Equals("Game"))
        {
            UnpauseGame();
        }
    }

    public void ShowQuitPanel()
    {
        QuitPanel.Show();
        panelsToDisable.ForEach(panel =>
        {
            var children = panel.GetComponentsInChildren<dfControl>();

            foreach (var child in children)
            {
                child.IsInteractive = false;
            }

        });

        if (Application.loadedLevelName.Equals("Game"))
        {
            PauseGame();
        }
    }

	void PauseGame()
	{
		GlobalPreferences.currentState = GameState.PAUSED;
	}

    void UnpauseGame()
    {
        GlobalPreferences.currentState = GameState.PLAYING;
    }
}
