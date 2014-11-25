using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkipTutorial : MonoBehaviour 
{

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GlobalPreferences.SkipTutorial = !GlobalPreferences.SkipTutorial;
	}

}
