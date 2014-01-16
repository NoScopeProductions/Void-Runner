using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static float distanceTraveled;
	public static Vector2 playerPos;
	public float speed;
	public float turnSpeed;
	private GameObject ship;
	public Transform[] deathExplosions;
	
	public int score;
	
	public float fuel;
	public float fuelDrain;
	
	public static Player instance;
	public enum PlayerState {ALIVE, DEAD, FALLING};
	public PlayerState state;
	
	public AudioClip pickUpSound;
	// Use this for initialization
	
	void Start () {
		ship = GameObject.Find("Body");
		
		score = 0;
		state = PlayerState.ALIVE;
			
		distanceTraveled = 0;
		playerPos = new Vector2(0f, 0f);
		instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(state != PlayerState.DEAD) {
			updatePlayer();	
		}
	}
	
	void updatePlayer()
	{
		//update the state
		state = checkAlive();
		//move forward
		transform.Translate(0f,0f, speed * Time.fixedDeltaTime);
		//keep track of position
		distanceTraveled = transform.localPosition.z;
		
		checkInput();
		
		//update x and y positions
		playerPos.x = transform.localPosition.x;
		playerPos.y = transform.localPosition.y;
		
		//drain fuel
		if(state == PlayerState.FALLING) {
			fuel -= fuelDrain * 10 * Time.deltaTime;	
		} else {
			fuel -= fuelDrain * Time.deltaTime;
		}

		if(fuel > 100) fuel = 100;
	}
	
	
	PlayerState checkAlive() {
		//first check the fuel
		if(fuel <= 0) {
			Kill();
			return PlayerState.DEAD;
		}
		
		//next check if the player is still on the tube
		RaycastHit hit;	
		
		//raycast downwards, if it hits, then the player is still in the tube.
		if(Physics.Raycast(transform.position, -transform.up, out hit, 100)){
			return PlayerState.ALIVE;
		}
		//if not, the player is falling, but the player has a bit of clearance to get back on the tube before his death.
		else {
			//the isInTube() function returns whether or not the player is within a bigger "box" than the tube itself
			//once the player has fallen out of this box, he is no longer safe.
			if (isInTube() == true){
				//transform.Translate(-transform.up, Space.World);
				return PlayerState.FALLING;
			}
			else {
				Kill();
				return PlayerState.DEAD;
			}
		}
	}
	
	
	bool isInTube(){
		if(playerPos.x > 50 || playerPos.x < -50){
			return false;
		}
		else if(playerPos.y > 50 || playerPos.y < -50){
			return false;
		}

		return true;
	}
	
	void Kill(){
		iTween.Stop();
		
		//create the explosion effects
		for(int i = 0; i < deathExplosions.Length; i++) {
			Instantiate(deathExplosions[i], ship.transform.parent.position, Quaternion.identity);
		}

		//stop rendering the ship model
		Destroy(ship);
		
		//TEMP - return to main menu after 2 seconds, to be replaced with end game menu.
		//TODO - Invoke end game menu here.
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
		CheckCollisionType(col);
	}

	private void CheckCollectibleType (string type) {
		if (type == "Collectible_Fuel") {
			fuel += CollectibleRewards.FUEL_GAIN;
			score += CollectibleRewards.SCORE_FUEL;
			Debug.Log ("Picked up Fuel");
		}
		else if (type == "Collectible_Speed") {
			score += CollectibleRewards.SCORE_SHIELD;
			Debug.Log ("Picked up Speed");
		}
		else if (type == "Collectible_Shield") {
			score += CollectibleRewards.SCORE_SPEED;
			Debug.Log ("Picked up Shield");
		}
	}

	void CheckCollisionType (Collider col)
	{
		if (col.tag.Contains("Collectible")) {
			Debug.Log ("Found Collectible");
			//these actions are applied regardless of the type of pickup
			audio.clip = pickUpSound;
			audio.Play ();

			//TODO - Recycle the object here

			CheckCollectibleType (col.tag);
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
	
	//helper function for the camera rotation script
	public void setPos(float x, float y) {
		transform.localPosition = new Vector3(x, y, distanceTraveled);
	}	
}


