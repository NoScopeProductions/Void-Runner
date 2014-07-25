using UnityEngine;
using System.Collections;

public class Button_Play_Script : MonoBehaviour 
{
	public GameObject SelectionMenu;
    public GameObject MainMenu;
    public AsteroidManager DebrisManager;

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
        //TODO: I REALLY want to figure this out...
        //iTween.FadeTo(SelectionMenu, 1f, 0.7f);
        //iTween.FadeTo(MainMenu, iTween.Hash("time", 0.7f, "alpha", 0f, "oncomplete", "SetActive", "oncompleteparams", false));
		SelectionMenu.SetActive (true);
        MainMenu.SetActive(false);
        DebrisManager.IsActive = false;
        
	}
}
