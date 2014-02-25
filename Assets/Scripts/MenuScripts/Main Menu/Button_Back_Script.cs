using UnityEngine;
using System.Collections;

public class Button_Back_Script : MonoBehaviour 
{
	public GameObject MainMenuGameObject;
	public GameObject SettingsMenu;
	// Use this for initialization
	void Start () 
	{
	}

	void OnMouseUp()
	{
		MainMenuGameObject.SetActive(true);
		SettingsMenu.SetActive(false);
	}

}
