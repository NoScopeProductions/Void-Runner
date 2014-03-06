﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static float distanceTraveled;
	public static Vector2 playerPos;
	public float speed;
	public float turnSpeed;
	private GameObject ship;
	public Transform[] deathExplosions;

	public GameObject playerCamera;
	public int score;
	
	public float fuel;
	public float fuelDrain;
	
	public static Player instance;
	public enum PlayerState {ALIVE, DEAD, FALLING, BOOSTING, DEACTIVATING_BOOST};
	public PlayerState state;

	public enum PowerUps {NONE, TURBO_BOOST, SHIELD};

	public PowerUps activePowerUp;

	public float boostDistance;

	public Vector3 boostIntensity;

	private Vector3 initialPos;

	private Vector3 initialCameraPos;
	
	public AudioClip pickUpSound;

	const float LANDING_DISTANCE = 36f;

	public void Start () 
	{
		init();
	}


	private void init() {
		ship = GameObject.Find ("Body"); //This needs to be decoupled

		score = 0;
		state = PlayerState.ALIVE;
		distanceTraveled = 0;

		playerPos = new Vector2 (0f, 0f); //this needs to be removed
		instance = this; //this needs to be removed

		activePowerUp = PowerUps.NONE;
	}
	
	// Update is called once per frame
	public void Update() 
	{
		if(state != PlayerState.DEAD) 
		{
			updatePlayer();	
		}
	}
	
	private void updatePlayer() 
	{
		//update the state
		state = checkAlive();
		//move forward
		transform.Translate(0f,0f, speed * Time.deltaTime);
		distanceTraveled = transform.localPosition.z; //keep track of position

		checkInput();		

		if(state == PlayerState.BOOSTING) 
		{
			Boost();
		}

		if(state == PlayerState.DEACTIVATING_BOOST) 
		{
			DeactivateBoost();
		}

		//keep track of position
		playerPos.x = transform.localPosition.x;
		playerPos.y = transform.localPosition.y;
		
		//drain fuel
		DrainFuel();
	}

	private void DrainFuel() {
		if(state == PlayerState.BOOSTING) 
		{
			fuel += fuelDrain * Time.deltaTime;
		}
		else if (state == PlayerState.FALLING) 
		{
			fuel -= fuelDrain * 10 * Time.deltaTime;
		}
		else 
		{
			fuel -= fuelDrain * Time.deltaTime;
		}

		if(fuel > 100) fuel = 100;
	}	
	
	private PlayerState checkAlive() 
	{
		//don't need to check the state while we're boosting.
		if(state == PlayerState.BOOSTING) return PlayerState.BOOSTING;
		if(state == PlayerState.DEACTIVATING_BOOST) return PlayerState.DEACTIVATING_BOOST;
		//first check the fuel
		if(fuel <= 0) 
		{
			Kill();
			return PlayerState.DEAD;
		}
		
		//next check if the player is still on the tube
		RaycastHit hit;	
		
		//raycast downwards, if it hits, then the player is still in the tube.
		if(Physics.Raycast(transform.position, -transform.up, out hit, 100))
		{
			return PlayerState.ALIVE;
		}
		//if not, the player is falling, but the player has a bit of clearance to get back on the tube before his death.
		else 
		{
			if (isInTube()) 
			{
				//transform.Translate(-transform.up, Space.World);
				return PlayerState.FALLING;
			}
			else 
			{
				Kill();
				return PlayerState.DEAD;
			}
		}
	}
	
	
	private bool isInTube() {
		if(playerPos.x > 50 || playerPos.x < -50)
		{
			return false;
		}
		else if(playerPos.y > 50 || playerPos.y < -50)
		{
			return false;
		}

		return true;
	}
	
	private void Kill() {
		iTween.Stop();
		
		//create the explosion effects
		for(int i = 0; i < deathExplosions.Length; i++) 
		{
			Instantiate(deathExplosions[i], ship.transform.parent.position, Quaternion.identity);
		}

		//stop rendering the ship model
		Destroy(ship);
		
		//TEMP - return to main menu after 2 seconds, to be replaced with end game menu.
		//TODO - Invoke end game menu here.
		Invoke("loadMenu", 2f);
		
	}
	
	private void loadMenu() 
	{
		Application.LoadLevel("Menu");	
	}
	
	private void checkInput() {
		//don't check input when the players is boosting.
		if(state == PlayerState.BOOSTING) return;
		if(state == PlayerState.DEACTIVATING_BOOST) return;

		//TODO - Implement Touch Controls Here
		if(Input.GetKey(KeyCode.LeftArrow)) 
		{
			MoveLeft();
		} 
		else if(Input.GetKey(KeyCode.RightArrow)) 
		{
			MoveRight();
		} 	
	}
	
	public void OnTriggerEnter(Collider col) 
	{
		CheckCollisionType(col);
	}

	private void CheckCollectibleType (string type) {
		if (type == "Collectible_Fuel") 
		{
			fuel += CollectibleRewards.FUEL_GAIN;
			score += CollectibleRewards.SCORE_FUEL;
		}
		else if (type == "Collectible_Speed") 
		{
			ActivateBoost();
		}
		else if (type == "Collectible_Shield") 
		{
			//TODO - ActivateShield();
			score += CollectibleRewards.SCORE_SHIELD;
		}
	}

	void ActivateBoost() {
		state = PlayerState.BOOSTING;
		initialPos = transform.position;
		initialCameraPos = playerCamera.transform.localPosition;
		activePowerUp = PowerUps.TURBO_BOOST;

		score += CollectibleRewards.SCORE_SPEED;

		iTween.ShakeRotation(playerCamera, boostIntensity, 6f);
	}

	private void DeactivateBoost() {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3 (initialPos.x, initialPos.y, transform.position.z), speed * Time.deltaTime);
		ResetCamera();
		if (transform.position.x == initialPos.x && transform.position.y == initialPos.y) 
		{
			state = PlayerState.ALIVE;
			activePowerUp = PowerUps.NONE;
			boostDistance = 300;
		}
	}

	private void Boost() 
	{
		transform.position = Vector3.MoveTowards(transform.position, new Vector3 (0, 0, transform.position.z), speed * Time.deltaTime);
		boostDistance -= speed * Time.deltaTime;
		transform.Translate (0f, 0f, speed * Time.deltaTime);
		WoobleCamera();

		if (boostDistance <= 0 && CanLand()) 
		{
			state = PlayerState.DEACTIVATING_BOOST;
		}
	}

	private bool CanLand()
	{
		RaycastHit hit;
		Vector3 aheadPos = transform.position;
		aheadPos.z += LANDING_DISTANCE;
		if(Physics.Raycast(aheadPos, -transform.up, out hit, 100))
		{
			return true;
		}

		return false;
	}

	private void WoobleCamera()
	{
		float noise = Mathf.PerlinNoise(Time.time, Time.time);
		playerCamera.transform.localPosition = Vector3.MoveTowards (playerCamera.transform.localPosition, new Vector3 (noise - 0.5f, noise + 1f, playerCamera.transform.localPosition.z), Time.deltaTime);
	}

	void ResetCamera()
	{
		playerCamera.transform.localPosition = Vector3.MoveTowards (playerCamera.transform.localPosition, new Vector3 (initialCameraPos.x, initialCameraPos.y, playerCamera.transform.localPosition.z), Time.deltaTime);
	}

	void CheckCollisionType(Collider col) 
	{
		if (col.tag.Contains("Collectible")) 
		{
			//these actions are applied regardless of the type of pickup
			audio.clip = pickUpSound;
			audio.Play();

			//recycle the collectible
			//TODO - Perhaps this recycling needs to be defined in the collectible's class, as opposed to here.
			Vector3 newPos = col.transform.position;
			newPos.z -= 100;
			col.transform.position = newPos;

			CheckCollectibleType(col.tag);
		}
	}
	
	void MoveLeft() 
	{
		switch(CameraRotate.rotation) 
		{
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
	
	void MoveRight() 
	{
		switch(CameraRotate.rotation) 
		{
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
	public void setPos(float x, float y) 
	{
		transform.localPosition = new Vector3(x, y, distanceTraveled);
	}	
}


