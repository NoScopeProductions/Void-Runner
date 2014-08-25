using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataEvents : MonoBehaviour 
{
    public dfLabel ScoreLabel;

	public void OnTextChanged( dfControl control, string value )
	{
		// Add event handler code here
		//Debug.Log( "TextChanged" );
        /*
        float score = Convert.ToSingle(value);

        if (score > PlayerPrefs.GetFloat(GlobalPreferences.HIGH_SCORE))
        {
            ScoreLabel.BottomColor = Color.yellow;
            ScoreLabel.Text = "New High Score!";
        }
         * */
	}

}
