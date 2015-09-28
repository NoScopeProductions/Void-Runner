using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsMenu : MonoBehaviour 
{

    public AsteroidManager asteroidManager;
    public GameObject CreditsPlane;

    private Vector3 DefaultPosition = new Vector3(0f, -478f, 185f);
    private Vector3 EndPosition = new Vector3(0f, 1000f, 185f);

	void OnEnable () 
	{
		Vector3[] TweenPath = {DefaultPosition, EndPosition};
        asteroidManager.IsActive = false;
		iTween.MoveTo(CreditsPlane, iTween.Hash("path", TweenPath, "easetype", iTween.EaseType.easeInOutSine, "time", 40f));

	}

    void OnDisable()
    {
		iTween.Stop ();
		CreditsPlane.transform.position = DefaultPosition;
        asteroidManager.IsActive = true;
    }


}