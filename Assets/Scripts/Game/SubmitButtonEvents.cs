using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class SubmitButtonEvents : MonoBehaviour 
{
    public Player PlayerObject;
    public dfTextbox NameEntry;

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        //Use Parse to save this score to the global rankings.
        ParseObject HighScoreObject = new ParseObject("HighScoreObject");

        HighScoreObject["Name"] = NameEntry.Text;
        HighScoreObject["Score"] = PlayerObject.Score;
        HighScoreObject.SaveAsync();
	}
}
