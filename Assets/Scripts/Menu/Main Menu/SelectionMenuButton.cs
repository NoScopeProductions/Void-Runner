﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionMenuButton : MonoBehaviour {

    public GlobalPreferences.SHIP WhichShip;

    private void Activate()
    {
        GlobalPreferences.shipSelected = WhichShip;
        Application.LoadLevel("Game");
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
