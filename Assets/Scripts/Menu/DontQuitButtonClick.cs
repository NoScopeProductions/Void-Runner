using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DontQuitButtonClick : MonoBehaviour
{
    public BackKeyListener keyListener;
    public dfPanel quitPanel;

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        keyListener.HideQuitPanel();
        keyListener.QuitPanelShowing = false;
        
        if (Application.loadedLevelName.Equals("Game"))
        {
            GlobalPreferences.TogglePause();
        }
	}
}
