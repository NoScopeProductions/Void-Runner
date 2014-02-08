using UnityEngine;
using System.Collections;

public class Button_Play_Script : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseUp()
	{
		Application.LoadLevel("Game");
		Debug.Log("Clicked Play!");
	}
}
