using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static float distanceTraveled;
	public static Vector2 playerPos;
	public float speed;
	public float turnSpeed;
	public int score;
	
	public static Player instance;
	public enum PlayerState {ALIVE, DEAD};
	public PlayerState state;
	
	public AudioClip pickUpSound;
	// Use this for initialization
	void Start () {
		//transform.constantForce.relativeForce = new Vector3(0f,0f,speed);
		score = 0;
		state = PlayerState.ALIVE;
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
		
		
		RaycastHit hit;	
		Debug.DrawRay(transform.position, -transform.up, Color.cyan);
		if(Physics.Raycast(transform.position, -transform.up,out hit, 100))
		{
			state = PlayerState.ALIVE;
		}
		else
		{
			state = PlayerState.DEAD;
			transform.Translate(-transform.up, Space.World);
		}
		
		//Uncomment this and checkgrounded below to do
		// some kind of death / falling animation
	}
	
	void checkInput() {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			MoveLeft();
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			MoveRight();
		}
	}
	
	void OnTriggerEnter(Collider col) {
		if(col.tag == "Collectible") {
			audio.clip = pickUpSound;
			audio.Play();
			score++;
			Debug.Log("Hit Collectible");
			//temp, move the collectible back so it looks like it disappears and get recycled.
			Vector3 newPos = col.transform.localPosition;
			newPos.z -= 50;
			col.transform.localPosition = newPos;
		}
	}
	
	void MoveLeft() {		
		switch(CameraRotate.rotation) {
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, 0f, 0f, Space.World);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(turnSpeed * Time.fixedDeltaTime, 0f, 0f, Space.World);
				break;
		}
		
	}
	
	void MoveRight() {
		switch(CameraRotate.rotation) {
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(turnSpeed * Time.fixedDeltaTime, 0f, 0f, Space.World);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, -turnSpeed * Time.fixedDeltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(-turnSpeed * Time.fixedDeltaTime, 0f, 0f, Space.World);
				break;
		}
		
	}
	
	public void setPos(float x, float y) {
		transform.localPosition = new Vector3(x, y, distanceTraveled);
	}	
}


