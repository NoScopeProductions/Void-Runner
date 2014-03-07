using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float distanceTraveled;
	public float speed;
	public float turnSpeed;
	public GameObject ShipBody;
	public Transform[] DeathExplosions;

	public GameObject PlayerCamera;
	public int Score;
	
	public float Fuel;
	public float FuelDrain;
	
	public enum PlayerState {ALIVE, DEAD, FALLING, BOOSTING, DEACTIVATING_BOOST};
    public enum PowerUps { NONE, TURBO_BOOST, SHIELD };

	public PlayerState State;

	public PowerUps ActivePowerUp;
    
	public const float BOOST_DISTANCE = 300;

    private float BoostDistanceTraveled;
    public Vector3 BoostIntensity;

	private Vector3 BoostInitialPos;
	private Vector3 BoostInitialCameraPos;
	
	public AudioClip PickUpSound;

	const float LANDING_DISTANCE = 36f;

	public void Start () 
	{
		Init();
	}

	private void Init() {
		Score = 0;
		State = PlayerState.ALIVE;
        distanceTraveled = 0;
        BoostDistanceTraveled = 0;
		ActivePowerUp = PowerUps.NONE;
	}

	public void Update() 
	{
		if(State != PlayerState.DEAD) 
		{
			updatePlayer();	
		}
	}
	
	private void updatePlayer() 
	{
		State = checkAlive();
		
		transform.Translate(0f,0f, speed * Time.deltaTime);

		distanceTraveled = transform.localPosition.z;

		checkInput();		

		if(State == PlayerState.BOOSTING) 
		{
			Boost();
		}

		if(State == PlayerState.DEACTIVATING_BOOST) 
		{
			DeactivateBoost();
		}

		DrainFuel();
	}

	private void DrainFuel() {
		if(State == PlayerState.BOOSTING) 
		{
			Fuel += FuelDrain * Time.deltaTime;
		}
		else if (State == PlayerState.FALLING) 
		{
			Fuel -= FuelDrain * 10 * Time.deltaTime;
		}
		else 
		{
			Fuel -= FuelDrain * Time.deltaTime;
		}

		if(Fuel > 100) Fuel = 100;
	}	
	
	private PlayerState checkAlive() 
	{
		//don't need to check the state while we're boosting.
		if(State == PlayerState.BOOSTING) return PlayerState.BOOSTING;
		if(State == PlayerState.DEACTIVATING_BOOST) return PlayerState.DEACTIVATING_BOOST;
		//first check the fuel
		if(Fuel <= 0) 
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
        if (transform.localPosition.x > 50 || transform.localPosition.x < -50)
		{
			return false;
		}
        else if (transform.localPosition.y > 50 || transform.localPosition.y < -50)
		{
			return false;
		}

		return true;
	}
	
	private void Kill() 
    {
		iTween.Stop();
		
		//create the explosion effects
		for(int i = 0; i < DeathExplosions.Length; i++) 
		{
			Instantiate(DeathExplosions[i], ShipBody.transform.parent.position, Quaternion.identity);
		}

		//stop rendering the ship model
		Destroy(ShipBody);
		
		//TEMP - return to main menu after 2 seconds, to be replaced with end game menu.
		//TODO - Invoke end game menu here.
		Invoke("loadMenu", 2f);
		
	}
	
	private void loadMenu() 
	{
		Application.LoadLevel("Menu");	
	}
	
	private void checkInput() 
    {
		//don't check input when the players is boosting.
		if(State == PlayerState.BOOSTING) return;
		if(State == PlayerState.DEACTIVATING_BOOST) return;

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

	private void CheckCollectibleType (string type) 
    {
		if (type == "Collectible_Fuel") 
		{
			Fuel += CollectibleRewards.FUEL_GAIN;
			Score += CollectibleRewards.SCORE_FUEL;
		}
		else if (type == "Collectible_Speed") 
		{
			ActivateBoost();
		}
		else if (type == "Collectible_Shield") 
		{
			//TODO - ActivateShield();
			Score += CollectibleRewards.SCORE_SHIELD;
		}
	}

    private void CheckCollisionType(Collider col)
    {
        if (col.tag.Contains("Collectible"))
        {
            //these actions are applied regardless of the type of pickup
            audio.clip = PickUpSound;
            audio.Play();

            //recycle the collectible
            //TODO - Perhaps this recycling needs to be defined in the collectible's class, as opposed to here.
            Vector3 newPos = col.transform.position;
            newPos.z -= 100;
            col.transform.position = newPos;

            CheckCollectibleType(col.tag);
        }
    }

    private void Boost()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), speed * Time.deltaTime);
        BoostDistanceTraveled -= speed * Time.deltaTime;
        transform.Translate(0f, 0f, speed * Time.deltaTime);
        WobbleCamera();

        if (BoostDistanceTraveled <= 0 && CanLand())
        {
            State = PlayerState.DEACTIVATING_BOOST;
        }
    }

	private void ActivateBoost() 
    {
        BoostDistanceTraveled = BOOST_DISTANCE;
		State = PlayerState.BOOSTING;
        ActivePowerUp = PowerUps.TURBO_BOOST;

		BoostInitialPos = transform.position;
		BoostInitialCameraPos = PlayerCamera.transform.localPosition;
		
		Score += CollectibleRewards.SCORE_SPEED;

		iTween.ShakeRotation(PlayerCamera, BoostIntensity, 6f); //Maybe not needed after particles are implemented
	}

	private void DeactivateBoost() 
    {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3 (BoostInitialPos.x, BoostInitialPos.y, transform.position.z), speed * Time.deltaTime);
		ResetCamera();
		if (transform.position.x == BoostInitialPos.x && transform.position.y == BoostInitialPos.y) 
		{
			State = PlayerState.ALIVE;
			ActivePowerUp = PowerUps.NONE;
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

	private void WobbleCamera()
	{
		float noise = Mathf.PerlinNoise(Time.time, Time.time);
		PlayerCamera.transform.localPosition = Vector3.MoveTowards (PlayerCamera.transform.localPosition, new Vector3 (noise - 0.5f, noise + 1f, PlayerCamera.transform.localPosition.z), Time.deltaTime);
	}

	private void ResetCamera()
	{
		PlayerCamera.transform.localPosition = Vector3.MoveTowards (PlayerCamera.transform.localPosition, new Vector3 (BoostInitialCameraPos.x, BoostInitialCameraPos.y, PlayerCamera.transform.localPosition.z), Time.deltaTime);
	}
	
	private void MoveLeft() 
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
	
	private void MoveRight() 
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
	
	public void setPos(float x, float y) 
	{
		transform.localPosition = new Vector3(x, y, distanceTraveled);
	}	
}