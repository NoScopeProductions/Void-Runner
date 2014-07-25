using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour 
{
	public GameObject MainMenu;
    public GameObject SelectionMenu;

    public AsteroidManager DebrisManager;
	
	private void Activate()
	{
        DebrisManager.IsActive = true;
        MainMenu.SetActive(true);
        iTween.FadeTo(MainMenu, 1f, 0.7f);

        iTween.FadeTo(SelectionMenu, iTween.Hash("time", 0.7f, "alpha", 0f, "oncomplete", "HideSelectionMenu", "oncompletetarget", gameObject));
	}

    private void HideSelectionMenu()
    {
        SelectionMenu.SetActive(false);
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
