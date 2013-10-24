using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static float distanceTraveled;
	public static Vector2 playerPos;
	public float speed;
	public float turnSpeed;
	
	public static Player instance;
	// Use this for initialization
	void Start () {
		//transform.constantForce.relativeForce = new Vector3(0f,0f,speed);
		
		distanceTraveled = 0;
		playerPos = new Vector2(0f, 0f);
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f,0f, speed * Time.fixedDeltaTime);
		distanceTraveled = transform.localPosition.z;
		checkInput();
		playerPos.x = transform.localPosition.x;
		playerPos.y = transform.localPosition.y;
		
	}
	
	void checkInput() {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			MoveLeft();
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			MoveRight();
		}
	}
	
	void MoveLeft() {
		switch(CameraRotate.rotation) {
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, 0f, 0f);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, 0f, speed);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(0f, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, -turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(0f, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(turnSpeed * Time.fixedDeltaTime, 0f, 0f);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, 0f, speed);
				break;
		}
	}
	
	void MoveRight() {
		switch(CameraRotate.rotation) {
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(turnSpeed * Time.fixedDeltaTime, 0f, 0f);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, 0f, speed);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, -turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(0f, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(0f, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, -turnSpeed, 0f);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, 0f, 0f);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, 0f, speed);
				break;
		}	
	}
	
	public void setPos(float x, float y) {
		//Debug.Log("setting player Pos");
		transform.localPosition = new Vector3(x, y, distanceTraveled);
	}
}


