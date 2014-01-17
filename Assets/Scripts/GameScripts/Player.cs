﻿using UnityEngine;
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
	public enum PlayerState {ALIVE, DEAD, FALLING, ACTIVATING_BOOST, DEACTIVATING_BOOST};
	public PlayerState state;

	public enum PowerUps {NONE, TURBO_BOOST, SHIELD};

	public PowerUps activePowerUp;

	public float boostDistance;
	private Vector3 initialPos;
	
	public AudioClip pickUpSound;
	// Use this for initialization
	
	void Start () {
		init();
	}

	void init() {
		ship = GameObject.Find ("Body");
		score = 0;
		state = PlayerState.ALIVE;
		distanceTraveled = 0;
		playerPos = new Vector2 (0f, 0f);
		instance = this;
		activePowerUp = PowerUps.NONE;
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

		if(state != PlayerState.ACTIVATING_BOOST) {
			checkInput();
		}

		//update x and y positions
		if(state == PlayerState.ACTIVATING_BOOST) {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0,transform.position.z), speed * Time.deltaTime);
			boostDistance -= speed * Time.deltaTime;
			transform.Translate(0f,0f, speed * Time.fixedDeltaTime);

			if(boostDistance <= 0) {
				state = PlayerState.DEACTIVATING_BOOST;
			}
		}

		if(state == PlayerState.DEACTIVATING_BOOST) {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(initialPos.x, initialPos.y, transform.position.z), speed * Time.deltaTime);

			if(transform.position.x == initialPos.x && transform.position.y == initialPos.y) {
				state = PlayerState.ALIVE;
				boostDistance = 300;
			}
		}


		playerPos.x = transform.localPosition.x;
		playerPos.y = transform.localPosition.y;
		
		//drain fuel
		DrainFuel();
	}

	void DrainFuel() {
		if (state == PlayerState.FALLING) {
			fuel -= fuelDrain * 10 * Time.deltaTime;
		}
		else {
			fuel -= fuelDrain * Time.deltaTime;
		}

		if(fuel > 100) fuel = 100;
	}	
	
	PlayerState checkAlive() {
		if(state == PlayerState.ACTIVATING_BOOST) return PlayerState.ACTIVATING_BOOST;
		if(state == PlayerState.DEACTIVATING_BOOST) return PlayerState.DEACTIVATING_BOOST;
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
			ActivateBoost();
			Debug.Log ("Picked up Speed");
		}
		else if (type == "Collectible_Shield") {
			score += CollectibleRewards.SCORE_SHIELD;
			Debug.Log ("Picked up Shield");
		}
	}

	void ActivateBoost ()
	{
		state = PlayerState.ACTIVATING_BOOST;
		initialPos = transform.position;
		activePowerUp = PowerUps.TURBO_BOOST;

		score += CollectibleRewards.SCORE_SPEED;
	}

	void CheckCollisionType (Collider col)
	{
		if (col.tag.Contains("Collectible")) {
			Debug.Log ("Found Collectible");
			//these actions are applied regardless of the type of pickup
			audio.clip = pickUpSound;
			audio.Play ();

			Vector3 newPos = col.transform.position;
			newPos.z -= 100;
			col.transform.position = newPos;

			CheckCollectibleType(col.tag);
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


