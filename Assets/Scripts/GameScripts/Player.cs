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
		distanceTraveled = 0;
		playerPos = new Vector2(0f, 0f);
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f, 0f, speed * Time.deltaTime);
		distanceTraveled = transform.localPosition.z;
		checkInput();
		playerPos.x = transform.localPosition.x;
		playerPos.y = transform.localPosition.y;
		
	}
	
	void checkInput() {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			//transform.RotateAround(Vector3.zero, transform.forward, Time.deltaTime * -turnSpeed);
			MoveLeft();
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			//transform.RotateAround(Vector3.zero, transform.forward, Time.deltaTime * turnSpeed);	
			MoveRight();
		}
	}
	
	void MoveLeft() {
		switch(CameraRotate.rotation) {
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(-turnSpeed * Time.deltaTime, 0f, 0f);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(-turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(-turnSpeed * Time.deltaTime, -turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, -turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(turnSpeed * Time.deltaTime, -turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(turnSpeed * Time.deltaTime, 0f, 0f);
				break;
		}
	}
	
	void MoveRight() {
		switch(CameraRotate.rotation) {
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(turnSpeed * Time.deltaTime, 0f, 0f);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(turnSpeed * Time.deltaTime, -turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, -turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(-turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(-turnSpeed * Time.deltaTime, -turnSpeed * Time.deltaTime, 0f);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(-turnSpeed * Time.deltaTime, 0f, 0f);
				break;
		}	
	}
	
	public void setPos(float x, float y) {
		//Debug.Log("setting player Pos");
		transform.localPosition = new Vector3(x, y, distanceTraveled);
	}
}


