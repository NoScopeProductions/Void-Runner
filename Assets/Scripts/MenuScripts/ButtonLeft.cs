using UnityEngine;
using System.Collections;

public class ButtonLeft : MonoBehaviour {
	
	private GameObject menumanager;
	private Vector3 menuspin = new Vector3(0.0f,-90.0f/360.0f,0.0f);
	
	void Awake() {
	menumanager = GameObject.Find("MenuManager2");
	}
	
	void menuMover(){
		iTween.RotateBy(menumanager,menuspin,2.0f);	
	}
	
	void OnMouseUp() {	
		menuMover();
    }

}
