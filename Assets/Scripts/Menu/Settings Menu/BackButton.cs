using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour 
{
	public GameObject MainMenu;
    public GameObject SelectionMenu;

    public AsteroidManager DebrisManager;

    public Texture2D BackButton_Down;
    public Texture2D BackButton_Normal;
	
	private void Activate()
	{
        DebrisManager.IsActive = true;
        MainMenu.SetActive(true);
        iTween.FadeTo(MainMenu, 1f, 0.4f);

        iTween.FadeTo(SelectionMenu, iTween.Hash("time", 0.3f, "alpha", 0f, "oncomplete", "HideSelectionMenu", "oncompletetarget", gameObject));
	}

    private void HideSelectionMenu()
    {
        SelectionMenu.SetActive(false);
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
                        break;
                    case TouchPhase.Stationary: //OnMouseEnter
                        //Switch to active guitexture here
                        guiTexture.texture = BackButton_Down;
                        break;
                    case TouchPhase.Ended: //OnMouseUp
                        guiTexture.texture = BackButton_Normal;
                        Activate();
                        break;
                }
            }
        }
    }

    #if UNITY_EDITOR
    public void OnMouseUp()
    {
        guiTexture.texture = BackButton_Normal;
        Activate();
    }

    public void OnMouseDown()
    {
        guiTexture.texture = BackButton_Down;
    }
    #endif

}
