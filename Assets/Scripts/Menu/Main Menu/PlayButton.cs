using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour 
{
	public GameObject SelectionMenu;
    public GameObject MainMenu;
    public AsteroidManager DebrisManager;

	void Activate()
	{
        //TODO: I REALLY want to figure this out...
        //iTween.FadeTo(SelectionMenu, 1f, 0.7f);
        //iTween.FadeTo(MainMenu, iTween.Hash("time", 0.7f, "alpha", 0f, "oncomplete", "SetActive", "oncompleteparams", false));
		SelectionMenu.SetActive (true);
        MainMenu.SetActive(false);
        DebrisManager.IsActive = false;
	}

    public void Update()
    {
        if (Input.touchCount <= 0) return;

        foreach (var touch in Input.touches)
        {
            if (guiTexture.HitTest(touch.position))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began: //OnMouseDown
                        //Don't think we need this phase
                        break;
                    case TouchPhase.Stationary: //OnMouseEnter
                        //Switch to active guitexture here
                        // guiTexture = PlayButtonDown
                        break;
                    case TouchPhase.Ended: //OnMouseUp
                        Activate();
                        break;
                }
            }
        }
    }


}
