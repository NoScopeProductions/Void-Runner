using UnityEngine;
using System.Collections;

public class MenuManager2 : MonoBehaviour {

	private GameObject menu2;
	private Vector3 farFrom = new Vector3(00.0f,00.0f,-99.0f);
	
	
	void Awake() {
		menu2 = GameObject.Find("MenuManager2");
		iTween.MoveBy(menu2,farFrom,2.0f);
	}
}
