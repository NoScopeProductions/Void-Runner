using UnityEngine;
using System.Collections;

public class Button_Back_Script : MonoBehaviour 
{
	public GameObject MainMenu;
	public GameObject SettingsMenu;
    public GameObject SelectionMenu;
	// Use this for initialization
	void Start () 
	{
	}

	void OnMouseUp()
	{
		MainMenu.SetActive(true);
		SettingsMenu.SetActive(false);
        SelectionMenu.SetActive(false);
	}

}
