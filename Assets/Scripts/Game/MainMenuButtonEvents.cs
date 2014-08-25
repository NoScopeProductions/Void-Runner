using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuButtonEvents : MonoBehaviour 
{
    
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        Invoke("LoadMenu", 1.5f);
	}

    private void LoadMenu()
    {
        Application.LoadLevel("Menu");
    }

}
