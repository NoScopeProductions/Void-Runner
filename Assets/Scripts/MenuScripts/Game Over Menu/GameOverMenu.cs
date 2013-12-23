using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnGUI () 
	{
		// Make a group on the center of the screen
		GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 200));
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

		// make a box so you can see where the group is on-screen.
		GUI.Box (new Rect (0,0,100,200), "Group is here");
		GUI.Label (new Rect (10,40,80,30), "Score:" + Player.instance.score.ToString());
		GUI.Label (new Rect (10,60,80,30), "Score:" + Player.instance.score.ToString());
		GUI.Label (new Rect (10,80,80,30), "Score:" + Player.instance.score.ToString());
		GUI.Button (new Rect (10,110,80,30), "Play Again");
		GUI.Button (new Rect (10,150,80,30), "Main Menu");

		// End the group we started above. This is very important to remember!
		GUI.EndGroup ();
	}
}
