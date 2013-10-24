﻿using UnityEngine;
using System.Collections;

public class Main_Play : MonoBehaviour {
	
	private GameObject rockmain;
	public Transform MultiExample;
	private Vector3 farTo = new Vector3(-100.0f,00.0f,00.0f);
	
	void Awake() {
	rockmain = GameObject.Find("Rock_Main");
		
	}

	void moveBody(){
		iTween.MoveBy(rockmain,farTo,1.0f);	
	}
	
	void boom(){
		Instantiate(MultiExample,rockmain.transform.position,Quaternion.identity);
		rockmain.renderer.enabled = false;
		Destroy(GameObject.Find ("Text CharSelect"));
		Destroy(GameObject.Find ("Text Play"));
		Destroy(GameObject.Find ("Text Settings"));
		Destroy(GameObject.Find ("Title"));
		
	}
	
    IEnumerator DoMoving()
    {
		moveBody();
		yield return new WaitForSeconds(1.0f);
		boom ();
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel("Game");
	}
	
	void OnMouseUp() {
		StartCoroutine(DoMoving());
	
	}
}
