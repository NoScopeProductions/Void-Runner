using UnityEngine;
using System.Collections;

public class Button_Settings_Script : MonoBehaviour 
{
	public GameObject MainMenu;
	public GameObject SettingsMenu;


	// Use this for initialization
	void Start () 
	{
	
	}

	void OnMouseUp()
	{
		SettingsMenu.SetActive(true);
		MainMenu.SetActive(false);
	}
}
