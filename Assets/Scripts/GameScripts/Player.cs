using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum PlayerState { ALIVE, DEAD, FALLING, BOOSTING, DEACTIVATING_BOOST };
    public enum PowerUps { NONE, TURBO_BOOST, SHIELD };

    private const float LANDING_DISTANCE = 36f;
    public const float BOOST_TIME = 3f;
    public const float TUTORIAL_DISTANCE = 750f;
	
	public float distanceTraveled;

    public float speedUpRate;
    public float turnRate;

    public const float MAX_SPEED = 60f;
    public const float MAX_TURN_RATE = 55f;

    public const float TURN_RATE_STARTING = 35f;

	public float speed;
	public float turnSpeed;

    public GameObject ShipBody_Remaker;
    public GameObject ShipBody_Default;
    public GameObject ShipBody_DSK;

	private GameObject SelectedShipBody;

	public Transform[] DeathExplosions;

    public GameObject Shield;

	public GameObject PlayerCamera;
	public int Score;
	
	public float Fuel;
	public float FuelDrain;
	
	public PlayerState State;
	public PowerUps ActivePowerUp;
    
    private float BoostTimeTraveled;
    public Vector3 BoostIntensity;

	private Vector3 BoostInitialPos;
	private Vector3 BoostInitialCameraPos;
	
	public AudioClip Sound_PickUp;
    public AudioClip Sound_DeactivateBoost;
    public AudioClip Sound_ActivateBoost;
    public AudioClip Sound_Explode;

    public AudioSource SoundManager;
    

	public void Start () 
	{
		Init();
	}

	private void Init() {
		Score = 0;
		State = PlayerState.ALIVE;
        distanceTraveled = 0;
        BoostTimeTraveled = 0;
		ActivePowerUp = PowerUps.NONE;

        switch (PlayerPreferences.shipSelected)
        {
            case PlayerPreferences.SHIP.DEFAULT:
                SelectedShipBody = ShipBody_Default;
                break;
            case PlayerPreferences.SHIP.DSK:
                SelectedShipBody = ShipBody_DSK;
                break;
            case PlayerPreferences.SHIP.REMAKER:
                SelectedShipBody = ShipBody_Remaker;
                break;
            default:
                SelectedShipBody = ShipBody_Default;
                break;
        }

        SelectedShipBody.SetActive(true);
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

        if (speed < MAX_SPEED)
        {
            if (distanceTraveled > TUTORIAL_DISTANCE + 200)
            {
                speed += speedUpRate * Time.deltaTime;
            }
        }
        else
        {
            speed = MAX_SPEED;
        }

		transform.Translate(0f,0f, speed * Time.deltaTime);

		distanceTraveled = transform.localPosition.z;

        if (distanceTraveled > TUTORIAL_DISTANCE)
        {
            checkInput();
            GetTouchInput();
        }
		
		if(State == PlayerState.BOOSTING) 
		{
			Boost();
		}

		if(State == PlayerState.DEACTIVATING_BOOST) 
		{
			DeactivateBoost();
		}

		DrainFuel();
        Score += 1;
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
        if (State == PlayerState.DEAD) return PlayerState.DEAD;
		
		if(Fuel <= 0) 
		{
            return Kill();
		}
		
		//next check if the player is still on the tube
		RaycastHit hit;	
		
		//raycast downwards, if it hits, then the player is still in the tube.
		if(Physics.Raycast(transform.position, -transform.up, out hit, 100))
		{
			return PlayerState.ALIVE;
		}
		else 
		{
			if (isInTube()) 
			{
				//transform.Translate(-transform.up, Space.World);
				return PlayerState.FALLING;
			}
			else 
			{
				return Kill();
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
	
	private PlayerState Kill() 
    {
		iTween.Stop();
		
		//create the explosion effects
		for(int i = 0; i < DeathExplosions.Length; i++) 
		{
			Instantiate(DeathExplosions[i], SelectedShipBody.transform.parent.position, Quaternion.identity);
		}

		//stop rendering the ship model
		Destroy(SelectedShipBody);
		
		//TEMP - return to main menu after 2 seconds, to be replaced with end game menu.
		//TODO - Invoke end game menu here.
		Invoke("loadMenu", 3f);

        SoundManager.PlayOneShot(Sound_Explode);

        return PlayerState.DEAD;
		
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
		
		if(Input.GetKey(KeyCode.LeftArrow)) 
		{
            if (turnSpeed < MAX_TURN_RATE) turnSpeed += turnRate * Time.deltaTime;
            else turnSpeed = MAX_TURN_RATE;

			MoveLeft();
		} 
		else if(Input.GetKey(KeyCode.RightArrow)) 
		{
            if (turnSpeed < MAX_TURN_RATE) turnSpeed += turnRate * Time.deltaTime;
            else turnSpeed = MAX_TURN_RATE;

			MoveRight();
		}

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            turnSpeed = TURN_RATE_STARTING;
        }
	}

	private void GetTouchInput ()
	{
		//don't check input when the players is boosting.
		if(State == PlayerState.BOOSTING) { return; }
		if (State == PlayerState.DEACTIVATING_BOOST) { return; }
				
		var touchCount = Input.touchCount;

		for ( var i = 0 ; i < touchCount ; i++ ) 
		{			
			var touch = Input.GetTouch (i);
			if(touch.position.x > Screen.width*0.5)
			{
                if (turnSpeed < MAX_TURN_RATE) turnSpeed += turnRate * Time.deltaTime;
                else turnSpeed = MAX_TURN_RATE;

				MoveRight();
			}
			if(touch.position.x < Screen.width*0.5)
			{
                if (turnSpeed < MAX_TURN_RATE) turnSpeed += turnRate * Time.deltaTime;
                else turnSpeed = MAX_TURN_RATE;

				MoveLeft();				
			}

            if (touch.phase == TouchPhase.Ended)
            {
                turnSpeed = TURN_RATE_STARTING;
            }
		}
	}
	
	public void OnTriggerEnter(Collider col) 
	{
		CheckCollisionType(col);
	}

    private void CheckCollisionType(Collider col)
    {
        if (col.tag.Contains("Collectible"))
        {
            //these actions are applied regardless of the type of pickup
            SoundManager.PlayOneShot(Sound_PickUp, 5);

            //recycle the collectible
            //TODO - Perhaps this recycling needs to be defined in the collectible's class, as opposed to here.
            Vector3 newPos = col.transform.position;
            newPos.z -= 100;
            col.transform.position = newPos;

            CheckCollectibleType(col.tag);
        }
        else if(col.tag.Contains("Rock"))
        {
            if (State != PlayerState.BOOSTING && State != PlayerState.DEACTIVATING_BOOST)
            {
                if (ActivePowerUp == PowerUps.SHIELD)
                {
                    DeactivateShield();
                    return;
                }
                State = Kill();
            }
        }

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
			ActivateShield();
			Score += CollectibleRewards.SCORE_SHIELD;
		}
	}

    private void ActivateShield()
    {
        ActivePowerUp = PowerUps.SHIELD;
        Shield.SetActive(true);
    }

    private void DeactivateShield()
    {
        ActivePowerUp = PowerUps.NONE;
        Shield.SetActive(false);
    }

    private void Boost()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), speed * Time.deltaTime);
        BoostTimeTraveled -= Time.deltaTime;
        transform.Translate(0f, 0f, 1.75f * speed * Time.deltaTime);
        WobbleCamera();

        if (BoostTimeTraveled <= 0 && CanLand())
        {
            State = PlayerState.DEACTIVATING_BOOST;
            SoundManager.PlayOneShot(Sound_DeactivateBoost);
        }
    }

	private void ActivateBoost() 
    {
        BoostTimeTraveled = BOOST_TIME;
		State = PlayerState.BOOSTING;
        ActivePowerUp = PowerUps.TURBO_BOOST;

        SoundManager.PlayOneShot(Sound_ActivateBoost);

		BoostInitialPos = transform.position;
		BoostInitialCameraPos = PlayerCamera.transform.localPosition;
		
		Score += CollectibleRewards.SCORE_SPEED;

		iTween.ShakeRotation(PlayerCamera, BoostIntensity, 5.5f); //Maybe not needed after particles are implemented
	}

	private void DeactivateBoost() 
    {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3 (BoostInitialPos.x, BoostInitialPos.y, transform.position.z), speed * Time.deltaTime);
		ResetCamera();
		if (transform.position.x == BoostInitialPos.x && transform.position.y == BoostInitialPos.y) 
		{
			State = PlayerState.ALIVE;
            DeactivateShield();
		}
	}

	private bool CanLand()
	{
		Vector3 aheadPos = transform.position;
		aheadPos.z += LANDING_DISTANCE;

        RaycastHit hit;

		if(Physics.Raycast(aheadPos, -transform.up, out hit, 100))
		{
            if (hit.transform.tag == "Tunnel")
            {
                return true;
            }
            else
            {
                Debug.Log(hit.collider.gameObject.tag);
            }
		}

		return false;
	}

	private void WobbleCamera()
	{
		float noise = Mathf.PerlinNoise(Time.time, Time.time);
		PlayerCamera.transform.localPosition = Vector3.MoveTowards (PlayerCamera.transform.localPosition, new Vector3 (noise - 0.5f, noise + 1f, PlayerCamera.transform.localPosition.z), 4 * Time.deltaTime);
	}

	private void ResetCamera()
	{
		PlayerCamera.transform.localPosition = Vector3.MoveTowards (PlayerCamera.transform.localPosition, new Vector3 (BoostInitialCameraPos.x, BoostInitialCameraPos.y, PlayerCamera.transform.localPosition.z), 4 * Time.deltaTime);
	}
	
	private void MoveLeft() 
	{
		switch(CameraRotate.rotation) 
		{
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(-turnSpeed * Time.deltaTime, 0f, 0f, Space.World);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(-turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(-turnSpeed * Time.deltaTime, -turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, -turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(turnSpeed * Time.deltaTime, -turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(turnSpeed * Time.deltaTime, 0f, 0f, Space.World);
				break;
		}
		
	}
	
	private void MoveRight() 
	{
		switch(CameraRotate.rotation) 
		{
			case CameraRotate.RotationState.BOTTOM:	
				transform.Translate(turnSpeed * Time.deltaTime, 0f, 0f, Space.World);
				break;
			case CameraRotate.RotationState.BOTTOM_LEFT:
				transform.Translate(turnSpeed * Time.deltaTime, -turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.BOTTOM_RIGHT:
				transform.Translate(turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.LEFT:
				transform.Translate(0f, -turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.RIGHT:
				transform.Translate(0f, turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP_RIGHT:
				transform.Translate(-turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP_LEFT:
				transform.Translate(-turnSpeed * Time.deltaTime, -turnSpeed * Time.deltaTime, 0f, Space.World);
				break;
			case CameraRotate.RotationState.TOP:	
				transform.Translate(-turnSpeed * Time.deltaTime, 0f, 0f, Space.World);
				break;
		}
		
	}
	
	public void setPos(float x, float y) 
	{
		transform.localPosition = new Vector3(x, y, distanceTraveled);
	}
}