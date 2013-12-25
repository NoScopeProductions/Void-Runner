using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static float distanceTraveled;
	public static Vector2 playerPos;
	public float speed;
	public float turnSpeed;
	private GameObject ship;
	public Transform deathExplosion;
	
	public int score;
	
	public float fuel;
	public float fuelDrain;
	
	public int collectibleScoreYield;
	public float collectibleFuelYield;
	
	public static Player instance;
	public enum PlayerState {ALIVE, DEAD};
	public PlayerState state;
	
	public AudioClip pickUpSound;
	// Use this for initialization
	void Awake(){
		ship = GameObject.Find("Mesh1");
	}
	void Start () {
		score = 0;
		state = PlayerState.ALIVE;
			
		distanceTraveled = 0;
		playerPos = new Vector2(0f, 0f);
		instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{

		updatePlayer();
		checkGrounded();
		checkFuel();
	}
	
	void updatePlayer()
	{
		if(state == PlayerState.ALIVE) {
			transform.Translate(0f,0f, speed * Time.fixedDeltaTime);
			distanceTraveled = transform.localPosition.z;
			checkInput();
			playerPos.x = transform.localPosition.x;
			playerPos.y = transform.localPosition.y;
			fuel -= fuelDrain * Time.deltaTime;
		} else {
			return;	
		}
	}
	
	bool isInTube(){
		
		if(playerPos.x > 50 || playerPos.x < -50){
			return false;
		}
		if(playerPos.y > 50 || playerPos.y < -50){
			return false;
		}
		else
			return true;
	
	}
	
	void Kill(){
		iTween.Stop();
		Destroy(rigidbody);
		ship.renderer.active = false;
		Instantiate(deathExplosion, ship.transform.position, Quaternion.identity);
		Destroy(GameObject.Find ("Mesh1"));
		
		Invoke("loadMenu", 2f);
		
	}
	
	void loadMenu() {
		Application.LoadLevel("Menu");	
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
			//Move the collectible back so it looks like it disappears and get recycled.
			Vector3 newPos = col.transform.localPosition;
			newPos.z -= 50;
			col.transform.localPosition = newPos;
			
			score += collectibleScoreYield;
			fuel += collectibleFuelYield;
			if(fuel > 100) fuel = 100;
		}
	}
	
	void checkGrounded() {
		RaycastHit hit;	
		Debug.DrawRay(transform.position, -transform.up, Color.cyan);
		if(Physics.Raycast(transform.position, -transform.up,out hit, 100)){
			state = PlayerState.ALIVE;
			}
		else{
			if (isInTube() == true){
				transform.Translate(-transform.up, Space.World);
			}
			if(state != PlayerState.DEAD) {
				if(isInTube() == false){
					state = PlayerState.DEAD;
					Kill();
				}
			}
		}	
	}
	
	void checkFuel() {
		if(fuel <= 0) {
			if(state != PlayerState.DEAD) {
				state = PlayerState.DEAD;
				Kill();
			}
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


