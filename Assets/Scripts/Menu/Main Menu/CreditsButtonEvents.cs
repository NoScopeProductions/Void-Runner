using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsButtonEvents : MonoBehaviour 
{
    public GameObject CreditsPlane;

    public AsteroidManager asteroidManager;

    private Vector3 DefaultPosition = new Vector3(0f, -478f, 185f);
    private Vector3 EndPosition = new Vector3(0f, 1000f, 185f);

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{

        Vector3[] TweenPath = new Vector3[] { DefaultPosition, EndPosition };
        asteroidManager.IsActive = false;
        iTween.MoveTo(CreditsPlane, iTween.Hash("path", TweenPath, "easetype", iTween.EaseType.easeInOutSine, "time", 40f));
	}

}
