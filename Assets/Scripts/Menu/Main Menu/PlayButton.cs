using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour 
{
	public GameObject SelectionMenu;
    public GameObject MainMenu;
    public AsteroidManager DebrisManager;

	void Activate()
	{
        SelectionMenu.SetActive(true);
        iTween.FadeTo(SelectionMenu, 1f, 0.4f);

        iTween.FadeTo(MainMenu, iTween.Hash("time", 0.3f, "alpha", 0f, "oncomplete", "HideMainMenu", "oncompletetarget", gameObject));
        DebrisManager.IsActive = false;
	}

    private void HideMainMenu()
    {
        MainMenu.SetActive(false);
    }

    #if UNITY_ANDROID
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
    #endif

    #if UNITY_EDITOR
    public void OnMouseUp()
    {
       Activate();
    }
    #endif
}
