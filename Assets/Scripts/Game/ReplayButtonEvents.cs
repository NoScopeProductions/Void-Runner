using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReplayButtonEvents : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        Application.LoadLevel(Application.loadedLevel);
	}

}
