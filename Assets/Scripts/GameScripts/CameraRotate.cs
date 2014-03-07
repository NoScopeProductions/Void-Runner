﻿using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
        
	public enum RotationState {BOTTOM, TOP, LEFT, RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, TOP_LEFT, TOP_RIGHT};

	public static RotationState rotation;
	public float tweenTime;
	public float rotateTime;
    public Player PlayerObject;

    private Vector2 PlayerPos;

	public void Start () {
        PlayerPos = new Vector2();
	    rotation = RotationState.BOTTOM;
	}

	public void Update () {
		//Round the x and y values to 1 decimal place to prevent errors
		PlayerPos.x = Mathf.Round(PlayerObject.transform.localPosition.x * 1000f) / 1000f;
        PlayerPos.y = Mathf.Round(PlayerObject.transform.localPosition.y * 1000f) / 1000f;

		checkCameraPosition();
	}

	private void checkCameraPosition() {
		checkBottomLeft();
		checkBottomRight();
		checkBottom();
		checkLeft();
		checkRight();
		checkTopRight();
		checkTopLeft();
		checkTop();
	}

	//for making the position checks a little easier
	private bool valueBetween(float x, float min, float max) {
		return (x >= min && x <= max);
	}

	/************************/
	//The following functions check if the camera needs to be rotated this frame.
	/************************/
	private void checkBottomLeft() {
		if (valueBetween (PlayerPos.x, -35f, -14f) && PlayerPos.y <= -14f) {
			if (rotation != RotationState.BOTTOM_LEFT) {
				toBottomLeft();
			}
		}
	}

	private void checkBottomRight() {
		if (valueBetween (PlayerPos.x, 14f, 34.5f) && PlayerPos.y <= -14f) {
			if (rotation != RotationState.BOTTOM_RIGHT) {
				toBottomRight();
			}
		}
	}

	private void checkBottom() {
		if (valueBetween (PlayerPos.x, -14f, 14f) && PlayerPos.y <= -34.5f) {
			if (rotation != RotationState.BOTTOM) {
				toBottom();
			}
		}
	}

	private void checkLeft() {
		if (valueBetween (PlayerPos.y, -14f, 14f) && PlayerPos.x <= -34.5f) {
			if (rotation != RotationState.LEFT) {
				toLeft();
			}
		}
	}

	private void checkRight() {
		if (valueBetween (PlayerPos.y, -14f, 14f) && PlayerPos.x >= 34.5f) {
			if (rotation != RotationState.RIGHT) {
				toRight();
			}
		}
	}

	private void checkTopRight(){
		if (valueBetween (PlayerPos.x, 14f, 34.5f) && PlayerPos.y >= 14f) {
			if (rotation != RotationState.TOP_RIGHT) {
				toTopRight();
			}
		}
	}

	private void checkTopLeft() {
		if (valueBetween (PlayerPos.x, -34.5f, -14f) && PlayerPos.y >= 14f) {
			if (rotation != RotationState.TOP_LEFT) {
				toTopLeft();
			}
		}
	}

	private void checkTop() {
		if (valueBetween (PlayerPos.x, -14f, 14f) && PlayerPos.y >= 34.5f) {
			if (rotation != RotationState.TOP) {
				toTop();
			}
		}
	}
	/************************/
	//The following functions tween the camera to the proper position
	/************************/
	private void toBottomLeft() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", -45, "time", tweenTime));
		if (rotation == RotationState.BOTTOM) PlayerObject.setPos (-14f, -34.5f);
		else if (rotation == RotationState.LEFT) PlayerObject.setPos (-34.5f, -14f);
		rotation = RotationState.BOTTOM_LEFT;
		iTween.RotateTo (PlayerObject.gameObject, iTween.Hash ("time", rotateTime, "z", -45));
	}

	private void toBottomRight() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 45, "time", tweenTime));
		if (rotation == RotationState.BOTTOM) PlayerObject.setPos (14f, -34.5f);
		else if (rotation == RotationState.RIGHT) PlayerObject.setPos (34.5f, -14f);
		rotation = RotationState.BOTTOM_RIGHT;
		iTween.RotateTo (PlayerObject.gameObject, iTween.Hash ("time", rotateTime, "z", 45));
	}

	private void toBottom() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "time", tweenTime));
		if (rotation == RotationState.BOTTOM_LEFT) PlayerObject.setPos (-14f, -34.5f);
		else if (rotation == RotationState.BOTTOM_RIGHT) PlayerObject.setPos (14f, -34.5f);
		rotation = RotationState.BOTTOM;
		iTween.RotateTo (PlayerObject.gameObject, iTween.Hash ("time", rotateTime, "z", 0));
	}

	private void toLeft() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", -90, "time", tweenTime));
		if (rotation == RotationState.BOTTOM_LEFT) PlayerObject.setPos (-34.5f, -14f);
		else if (rotation == RotationState.TOP_LEFT) PlayerObject.setPos (-34.5f, 14f);
		rotation = RotationState.LEFT;
		iTween.RotateTo (PlayerObject.gameObject, iTween.Hash ("time", rotateTime, "z", -90));
	}

	private void toRight() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 90, "time", tweenTime));
		if (rotation == RotationState.BOTTOM_RIGHT) PlayerObject.setPos (34.5f, -14f);
		else if (rotation == RotationState.TOP_RIGHT) PlayerObject.setPos (34.5f, 14f);
		rotation = RotationState.RIGHT;
		iTween.RotateTo (PlayerObject.gameObject, iTween.Hash ("time", rotateTime, "z", 90));
	}

	private void toTopRight () {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 135, "time", tweenTime));
		if (rotation == RotationState.RIGHT) PlayerObject.setPos (34.5f, 14f);
		else if (rotation == RotationState.TOP) PlayerObject.setPos (14f, 34.5f);
		rotation = RotationState.TOP_RIGHT;
		iTween.RotateTo (PlayerObject.gameObject, iTween.Hash ("time", rotateTime, "z", 135));
	}

	private void toTopLeft(){
		iTween.RotateTo (gameObject, iTween.Hash ("z", -135, "time", tweenTime));
		if (rotation == RotationState.TOP) PlayerObject.setPos (-14f, 34.5f);
		else if (rotation == RotationState.LEFT) PlayerObject.setPos (-34.5f, 14f);
		rotation = RotationState.TOP_LEFT;
		iTween.RotateTo (PlayerObject.gameObject, iTween.Hash ("time", rotateTime, "z", -135));
	}

	private void toTop() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 180, "time", tweenTime));
		if (rotation == RotationState.TOP_LEFT) PlayerObject.setPos (-14f, 34.5f);
		else if (rotation == RotationState.TOP_RIGHT) PlayerObject.setPos (14f, 34.5f);
		rotation = RotationState.TOP;
		iTween.RotateTo (PlayerObject.gameObject, iTween.Hash ("time", rotateTime, "z", 180));
	}

}