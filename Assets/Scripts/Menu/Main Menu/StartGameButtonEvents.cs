using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartGameButtonEvents : MonoBehaviour 
{
    public CameraFade cameraFade;
    public dfCoverflow selectionMenu;

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
        switch (selectionMenu.selectedIndex)
        {
            case 0:
                GlobalPreferences.shipSelected = GlobalPreferences.SHIP.REMAKER;
                break;
            case 1:
                GlobalPreferences.shipSelected = GlobalPreferences.SHIP.DEFAULT;
                break;
            case 2:
                GlobalPreferences.shipSelected = GlobalPreferences.SHIP.DSK;
                break;
        }

        cameraFade.FadeOut();
        Invoke("LoadGame", 1.5f);
	}

    private void LoadGame()
    {
        Application.LoadLevel("Game");
    }

}