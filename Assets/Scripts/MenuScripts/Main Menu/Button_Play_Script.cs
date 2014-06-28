using UnityEngine;
using System.Collections;

public class Button_Play_Script : MonoBehaviour 
{
	public GameObject SelectionMenu;
    public GameObject MainMenu;
    public DebrisSpawner DebrisManager;

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
        MainMenu.SetActive(false);
        DebrisManager.IsActive = false;
        
	}
}
