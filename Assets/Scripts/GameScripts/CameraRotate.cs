using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
        
public enum RotationState {BOTTOM, TOP, LEFT, RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, TOP_LEFT, TOP_RIGHT};

public static RotationState rotation;
public float tweenTime;
	public float rotateTime;
// Use this for initialization
void Start () {
        rotation = RotationState.BOTTOM;
}

// Update is called once per frame
void Update () {
		//We round the x and y values to 1 decimal place to prevent errors
		Player.playerPos.x = Mathf.Round(Player.playerPos.x * 1000f) / 1000f;
		Player.playerPos.y = Mathf.Round(Player.playerPos.y * 1000f) / 1000f;

        checkCameraPosition();
}

	//for making the update checks a little easier
	private bool valueBetween(float x, float min, float max) {
	        return (x >= min && x <= max);
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

	/************************/
	//The following functions check if the camera needs to be rotated this frame.
	/************************/
	private void checkBottomLeft() {
		if (valueBetween (Player.playerPos.x, -35f, -14f) && Player.playerPos.y <= -14f) {
			if (rotation != RotationState.BOTTOM_LEFT) {
				toBottomLeft();
			}
		}
	}

	private void checkBottomRight() {
		if (valueBetween (Player.playerPos.x, 14f, 34.5f) && Player.playerPos.y <= -14f) {
			if (rotation != RotationState.BOTTOM_RIGHT) {
				toBottomRight();
			}
		}
	}

	private void checkBottom() {
		if (valueBetween (Player.playerPos.x, -14f, 14f) && Player.playerPos.y <= -34.5f) {
			if (rotation != RotationState.BOTTOM) {
				toBottom();
			}
		}
	}

	private void checkLeft() {
		if (valueBetween (Player.playerPos.y, -14f, 14f) && Player.playerPos.x <= -34.5f) {
			if (rotation != RotationState.LEFT) {
				toLeft();
			}
		}
	}

	private void checkRight() {
		if (valueBetween (Player.playerPos.y, -14f, 14f) && Player.playerPos.x >= 34.5f) {
			if (rotation != RotationState.RIGHT) {
				toRight();
			}
		}
	}

	private void checkTopRight(){
		if (valueBetween (Player.playerPos.x, 14f, 34.5f) && Player.playerPos.y >= 14f) {
			if (rotation != RotationState.TOP_RIGHT) {
				toTopRight();
			}
		}
	}

	private void checkTopLeft() {
		if (valueBetween (Player.playerPos.x, -34.5f, -14f) && Player.playerPos.y >= 14f) {
			if (rotation != RotationState.TOP_LEFT) {
				toTopLeft();
			}
		}
	}

	private void checkTop() {
		if (valueBetween (Player.playerPos.x, -14f, 14f) && Player.playerPos.y >= 34.5f) {
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
		if (rotation == RotationState.BOTTOM) Player.instance.setPos (-14f, -34.5f);
		else if (rotation == RotationState.LEFT) Player.instance.setPos (-34.5f, -14f);
		rotation = RotationState.BOTTOM_LEFT;
		iTween.RotateTo (Player.instance.gameObject, iTween.Hash ("time", rotateTime, "z", -45));
	}

	private void toBottomRight() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 45, "time", tweenTime));
		if (rotation == RotationState.BOTTOM) Player.instance.setPos (14f, -34.5f);
		else if (rotation == RotationState.RIGHT) Player.instance.setPos (34.5f, -14f);
		rotation = RotationState.BOTTOM_RIGHT;
		iTween.RotateTo (Player.instance.gameObject, iTween.Hash ("time", rotateTime, "z", 45));
	}

	private void toBottom() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "time", tweenTime));
		if (rotation == RotationState.BOTTOM_LEFT) Player.instance.setPos (-14f, -34.5f);
		else if (rotation == RotationState.BOTTOM_RIGHT) Player.instance.setPos (14f, -34.5f);
		rotation = RotationState.BOTTOM;
		iTween.RotateTo (Player.instance.gameObject, iTween.Hash ("time", rotateTime, "z", 0));
	}

	private void toLeft() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", -90, "time", tweenTime));
		if (rotation == RotationState.BOTTOM_LEFT) Player.instance.setPos (-34.5f, -14f);
		else if (rotation == RotationState.TOP_LEFT) Player.instance.setPos (-34.5f, 14f);
		rotation = RotationState.LEFT;
		iTween.RotateTo (Player.instance.gameObject, iTween.Hash ("time", rotateTime, "z", -90));
	}

	private void toRight() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 90, "time", tweenTime));
		if (rotation == RotationState.BOTTOM_RIGHT) Player.instance.setPos (34.5f, -14f);
		else if (rotation == RotationState.TOP_RIGHT) Player.instance.setPos (34.5f, 14f);
		rotation = RotationState.RIGHT;
		iTween.RotateTo (Player.instance.gameObject, iTween.Hash ("time", rotateTime, "z", 90));
	}

	private void toTopRight () {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 135, "time", tweenTime));
		if (rotation == RotationState.RIGHT) Player.instance.setPos (34.5f, 14f);
		else if (rotation == RotationState.TOP) Player.instance.setPos (14f, 34.5f);
		rotation = RotationState.TOP_RIGHT;
		iTween.RotateTo (Player.instance.gameObject, iTween.Hash ("time", rotateTime, "z", 135));
	}

	private void toTopLeft(){
		iTween.RotateTo (gameObject, iTween.Hash ("z", -135, "time", tweenTime));
		if (rotation == RotationState.TOP) Player.instance.setPos (-14f, 34.5f);
		else if (rotation == RotationState.LEFT) Player.instance.setPos (-34.5f, 14f);
		rotation = RotationState.TOP_LEFT;
		iTween.RotateTo (Player.instance.gameObject, iTween.Hash ("time", rotateTime, "z", -135));
	}

	private void toTop() {
		iTween.RotateTo (gameObject, iTween.Hash ("z", 180, "time", tweenTime));
		if (rotation == RotationState.TOP_LEFT) Player.instance.setPos (-14f, 34.5f);
		else if (rotation == RotationState.TOP_RIGHT) Player.instance.setPos (14f, 34.5f);
		rotation = RotationState.TOP;
		iTween.RotateTo (Player.instance.gameObject, iTween.Hash ("time", rotateTime, "z", 180));
	}

}