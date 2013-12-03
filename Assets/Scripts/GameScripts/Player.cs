using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static float distanceTraveled;
	public static Vector2 playerPos;
	public float speed;
	public float turnSpeed;
	private GameObject ship;
	public Transform MultiExample;
	
	
	public static Player instance;
	public enum PlayerState {ALIVE, DEAD};
	public PlayerState state;
	// Use this for initialization
	void Awake(){
		ship = GameObject.Find("Mesh1");
	}
	void Start () {
		//transform.constantForce.relativeForce = new Vector3(0f,0f,speed);
		state = PlayerState.ALIVE;
		distanceTraveled = 0;
		playerPos = new Vector2(0f, 0f);
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0f,0f, speed * Time.fixedDeltaTime);
		distanceTraveled = transform.localPosition.z;
		
		if(intube() == true){
			checkInput();
			playerPos.x = transform.localPosition.x;
			playerPos.y = transform.localPosition.y;
		}
		
			RaycastHit hit;	
			Debug.DrawRay(transform.position, -transform.up, Color.cyan);
			if(Physics.Raycast(transform.position, -transform.up,out hit, 100)){
				state = PlayerState.ALIVE;
				}
			else{
				if (intube() == true){
				transform.Translate(-transform.up, Space.World);
				}
				if(state != PlayerState.DEAD) {
					if(intube() == false){
						kill ();
						state = PlayerState.DEAD;
					}
				}
				}	
	}
	bool intube(){
		
		if(playerPos.x > 50 || playerPos.x < -50){
			return false;
		}
		if(playerPos.y > 50 || playerPos.y < -50){
			return false;
		}
		else
			return true;
	
	}
	
	void kill(){
		
		ship.renderer.active = false;
		Instantiate(MultiExample,ship.transform.position,Quaternion.identity);
		Destroy(GameObject.Find ("Mesh1"));
		
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
}


