using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
	
	public enum RotationState {BOTTOM, TOP, LEFT, RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, TOP_LEFT, TOP_RIGHT};
	
	public static RotationState rotation;
	
	
	// Use this for initialization
	void Start () {
		rotation = RotationState.BOTTOM;
	}
	
	// Update is called once per frame
	void Update () {
		if(valueBetween(Player.playerPos.x, -35f, -14f) && Player.playerPos.y <= -14f) {
			if(rotation != RotationState.BOTTOM_LEFT) {
				rotation = RotationState.BOTTOM_LEFT;
				iTween.RotateTo(gameObject, iTween.Hash("z", -45, "time", 0.7f));
			}
		}
		else if(valueBetween(Player.playerPos.x, 14f, 34.5f) && Player.playerPos.y <= -14f) {
			if(rotation != RotationState.BOTTOM_RIGHT) {
				rotation = RotationState.BOTTOM_RIGHT;
				iTween.RotateTo(gameObject, iTween.Hash("z", 45, "time", 0.7f));
			}
		}
		else if(valueBetween(Player.playerPos.x, -14f, 14f) && Player.playerPos.y <= -34f) {
			if(rotation != RotationState.BOTTOM) {
				rotation = RotationState.BOTTOM;
				iTween.RotateTo(gameObject, iTween.Hash("z", 0, "time", 0.7f));
			}
		}
		
		
	}
	
	//for making the update checks a little easier
	private bool valueBetween(float x, float min, float max) {
		return (x > min && x < max);
	}
}
