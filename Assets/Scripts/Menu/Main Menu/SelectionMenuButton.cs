using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionMenuButton : MonoBehaviour {

    public GlobalPreferences.SHIP WhichShip;

    public CameraFade cameraFade;

    private void Activate()
    {
        GlobalPreferences.shipSelected = WhichShip;
        cameraFade.FadeOut();
        Invoke("LoadGame", 1.5f);
    }

    private void LoadGame()
    {
        Application.LoadLevel("Game");
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
                    case TouchPhase.Ended: //OnMouseUp
                        Activate();
                        break;
                }
            }
        }
    }

    #if UNITY_EDITOR
    public void OnMouseUp() 
    {
        Activate();
    }
    #endif

}
