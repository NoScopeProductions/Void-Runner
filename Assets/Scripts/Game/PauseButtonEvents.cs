using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseButtonEvents : MonoBehaviour 
{

    private bool Paused = false;

    public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        Activate();
	}

    public void Activate()
    {
        Paused = !Paused;

        if (Paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = GlobalPreferences.CurrentTimeScale;
        }
    }

}
