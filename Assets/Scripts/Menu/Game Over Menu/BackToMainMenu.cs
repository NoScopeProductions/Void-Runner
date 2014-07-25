using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour 
{
    #if UNITY_EDITOR
    public void OnMouseUp()
    {
        Application.LoadLevel("Menu");
    }
    #endif

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
                        Application.LoadLevel("Menu");
                        break;
                }
            }
        }
    }
    #endif

}
