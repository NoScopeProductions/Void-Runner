using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour 
{
	public GameObject MainMenu;
    public GameObject SelectionMenu;

    public AsteroidManager DebrisManager;
	// Use this for initialization
	void Start () 
	{
	}

	void OnMouseUp()
	{
		MainMenu.SetActive(true);
        SelectionMenu.SetActive(false);

        DebrisManager.IsActive = true;
	}

}
