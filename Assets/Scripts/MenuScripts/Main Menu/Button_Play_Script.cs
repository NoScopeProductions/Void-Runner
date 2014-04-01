using UnityEngine;
using System.Collections;

public class Button_Play_Script : MonoBehaviour 
{
	public GameObject SelectionMenu;

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
		SelectionMenu.SetActive (true);
	}
}
