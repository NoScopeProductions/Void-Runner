using UnityEngine;
using System.Collections;

public class SplashManager : MonoBehaviour 
{


	void Start() {
		iTween.FadeTo(gameObject, 0f, 1.5f);
		iTween.FadeTo(gameObject, iTween.Hash("alpha", 1f, "time", 1.5f, "delay", 3f));
		Invoke("loadMenu", 5f);
	}

	void loadMenu() {
		Application.LoadLevel("Menu");
	}
}