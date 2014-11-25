using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuitButtonClick : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
	    Application.Quit();
	}

}
