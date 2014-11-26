using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseButtonEvents : MonoBehaviour 
{
    public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GlobalPreferences.TogglePause();
	}
}
