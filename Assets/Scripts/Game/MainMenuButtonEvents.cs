using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuButtonEvents : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        Application.LoadLevel("Menu");
	}

}
