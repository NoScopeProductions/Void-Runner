using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayButtonEvents : MonoBehaviour 
{
    
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        Invoke("Reload", 1.5f);
	}

    private void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
