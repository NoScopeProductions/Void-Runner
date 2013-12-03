using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static float distanceTraveled;
	public static Vector2 playerPos;
	public float speed;
	public float turnSpeed;
	
	public static Player instance;
	public enum PlayerState {ALIVE, DEAD};
	public PlayerState state;
	// Use this for initialization
	void Start () {
		//transform.constantForce.relativeForce = new Vector3(0f,0f,speed);
		state = PlayerState.ALIVE;
		distanceTraveled = 0;
		playerPos = new Vector2(0f, 0f);
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		switch(state) {
			case PlayerState.ALIVE:	
				transform.Translate(0f,0f, speed * Time.fixedDeltaTime);
				distanceTraveled = transform.localPosition.z;
				checkInput();
				playerPos.x = transform.localPosition.x;
				playerPos.y = transform.localPosition.y;		
				break;
			case PlayerState.DEAD:
				//Trigger animation here?
				transform.Translate(-transform.up, Space.World);
				break;
		}
		
		checkGrounded();
	}
	
	void checkInput() {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			MoveLeft();
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			MoveRight();
		}
	}
	
	void MoveLeft() {
		//transform.Translate(-turnSpeed * Time.fixedDeltaTime, 0f, 0f);
		
		switch(CameraRotate.rotation) {
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, 0f, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, 0f, speed);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(0f, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(0f, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(turnSpeed * Time.fixedDeltaTime, 0f, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, 0f, speed);
				break;
		}
		
	}
	
	void MoveRight() {
		//transform.Translate(turnSpeed * Time.fixedDeltaTime, 0f, 0f);
		
		switch(CameraRotate.rotation) {
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(turnSpeed * Time.fixedDeltaTime, 0f, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, 0f, speed);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(turnSpeed, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(0f, -turnSpeed, speed);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(0f, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, turnSpeed, speed);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, -turnSpeed, 0f);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, 0f, 0f, Space.World);
				//transform.constantForce.relativeForce = new Vector3(-turnSpeed, 0f, speed);
				break;
		}
		
	}
	
	public void setPos(float x, float y) {
		//Debug.Log("setting player Pos");
		transform.localPosition = new Vector3(x, y, distanceTraveled);
	}	
	
	private void checkGrounded() {
		RaycastHit hit;	
		if(!Physics.Raycast(transform.position, -transform.up,out hit, 100))
		{			
			state = PlayerState.DEAD;
		}	
	}
}


