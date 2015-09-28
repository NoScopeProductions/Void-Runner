using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkipTutorial : MonoBehaviour 
{

    public void OnEnable()
    {
        var checkbox = GetComponent<dfCheckbox>();

        checkbox.IsChecked = GlobalPreferences.SkipTutorial;
    }

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        GlobalPreferences.SkipTutorial = !GlobalPreferences.SkipTutorial;
	}

}
