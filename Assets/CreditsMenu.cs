using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsMenu : MonoBehaviour {

    public AsteroidManager asteroidManager;
    public GameObject CreditsPlane;

    private Vector3 DefaultPosition = new Vector3(0f,-478f,185f);
    private Vector3 EndPosition = new Vector3(0f, 440f, 185f);

	void OnEnable () {
        asteroidManager.IsActive = false;
        CreditsPlane.transform.position = DefaultPosition;
        iTween.MoveTo(CreditsPlane, iTween.Hash("position", EndPosition, "easetype", iTween.EaseType.easeInOutSine, "time", 25f));

	}

    void OnDisable()
    {
        asteroidManager.IsActive = true;
    }
}
