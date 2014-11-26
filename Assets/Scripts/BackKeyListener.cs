using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackKeyListener : MonoBehaviour 
{
    public dfPanel QuitPanel;
    public bool QuitPanelShowing = false;
    public List<dfControl> panelsToDisable;
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

            if (Application.loadedLevelName.Equals("Game"))
            {
                GlobalPreferences.TogglePause();
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
    }
}
