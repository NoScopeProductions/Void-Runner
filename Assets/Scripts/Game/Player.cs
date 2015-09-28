using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public enum PlayerState { ALIVE, DEAD, OVER_GAP, BOOSTING, DEACTIVATING_BOOST };
    public enum PowerUps { NONE, SHIELD };

    public dfPanel GameOverMenu;
    public DangerFlash dangerFlash;

    private const float LANDING_DISTANCE = 32f;
    public const float BOOST_TIME = 3f;
    public const float TUTORIAL_DISTANCE = 750f;
	
    public float speedUpRate;
    public float turnRate;

    public const float MAX_SPEED = 60f;
    public const float MAX_TURN_RATE = 55f;

    public const float TURN_RATE_STARTING = 35f;

    public float SCORE_PER_SECOND;

	public float speed;
	public float turnSpeed;

    public GameObject ShipBody_Remaker;
    public GameObject ShipBody_Default;
    public GameObject ShipBody_DSK;

	private GameObject SelectedShipBody;

	public Transform[] DeathExplosions;

    public GameObject Shield;

	public GameObject PlayerCamera;
	public float Score;
	
	public float Fuel;
	public float FuelDrain;
    public float FuelDrainWhenFalling;

    public bool EnableGodMode;

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
    public AudioClip Sound_ShieldExplode;
    public AudioClip Sound_ShieldActivate;

    public AudioSource SoundManager;

    [HideInInspector]
    public int redPickupCount = 0;

    [HideInInspector]
    public int greenPickupCount = 0;

    [HideInInspector]
    public int bluePickupCount = 0 ;

    private Action CheckInput;

    private float HighScore;

	public void Start () 
	{
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.WP8Player:
                CheckInput = CheckTouchInput;
                break;

            default:
                CheckInput = CheckKeyboardInput;
                break;
        }

        GlobalPreferences.SetDefaultTimeScale();

		Init();
	}

	private void Init() {
		Score = 0;
        HighScore = PlayerPrefs.GetFloat(GlobalPreferences.HIGH_SCORE);

		State = PlayerState.ALIVE;
        BoostTimeTraveled = 0;
		ActivePowerUp = PowerUps.NONE;

        switch (GlobalPreferences.shipSelected)
        {
            case GlobalPreferences.SHIP.DEFAULT:
                SelectedShipBody = ShipBody_Default;
                break;
            case GlobalPreferences.SHIP.DSK:
                SelectedShipBody = ShipBody_DSK;
                break;
            case GlobalPreferences.SHIP.REMAKER:
                SelectedShipBody = ShipBody_Remaker;
                break;
            default:
                SelectedShipBody = ShipBody_Default;
                break;
        }

        SelectedShipBody.SetActive(true);

        if (GlobalPreferences.SkipTutorial)
        {
            transform.position = new Vector3(0, -34.5f, 365f);
        }
	}

	public void Update() 
	{
		if(State != PlayerState.DEAD) 
		{
			UpdatePlayer();	
		}
	}
	
	private void UpdatePlayer() 
	{
		State = CheckAlive();

        if (State == PlayerState.OVER_GAP)
        {
            dangerFlash.Enable();
        }
        else
        {
            dangerFlash.Disable();
        }


        if (speed < MAX_SPEED)
        {
            if (transform.position.z > TUTORIAL_DISTANCE)
            {
                speed += speedUpRate * Time.deltaTime;
            }
        }
        else
        {
            speed = MAX_SPEED;
        }

        MovePlayerForward();

        CheckInput();
		
		if(State == PlayerState.BOOSTING) 
		{
			Boost();
		}

		if(State == PlayerState.DEACTIVATING_BOOST) 
		{
			DeactivateBoost();
		}

		DrainFuel();

        if (transform.position.z > TUTORIAL_DISTANCE) { Score += SCORE_PER_SECOND * Time.deltaTime; }
	}

	private void DrainFuel() 
    {

        if (EnableGodMode) return;

		switch (State)
		{
		    case PlayerState.BOOSTING:
		        Fuel += 3 * FuelDrain * Time.deltaTime;
		        break;
		    case PlayerState.OVER_GAP:
		        Fuel -= FuelDrainWhenFalling * Time.deltaTime;
		        break;
		    default:
		        if (transform.position.z < TUTORIAL_DISTANCE)
		        {
		            Fuel -= FuelDrain * Time.deltaTime * 0.5f;
		        }
		        else 
		        {
		            Fuel -= FuelDrain * Time.deltaTime;
		        }
		        break;
		}

		if(Fuel > 100) Fuel = 100;
	}	
	
	private PlayerState CheckAlive() 
	{
		if(State == PlayerState.BOOSTING) return PlayerState.BOOSTING;
		if(State == PlayerState.DEACTIVATING_BOOST) return PlayerState.DEACTIVATING_BOOST;
        if(State == PlayerState.DEAD) return PlayerState.DEAD;
		
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
            return PlayerState.OVER_GAP;
		}
	}
	
	private PlayerState Kill() 
    {
        if (EnableGodMode) 
		{
			return PlayerState.ALIVE;
		}

		if (Math.Abs(Time.timeScale - 1.0f) > 0.001f) 
		{
			Time.timeScale = 1.0f;
		}
		//create the explosion effects
		foreach (var explosion in DeathExplosions)
		{
		    var ex = (Transform)Instantiate(explosion, SelectedShipBody.transform.parent.position, Quaternion.identity);
		    ex.parent = transform;
		}

		//stop rendering the ship model
		Destroy(SelectedShipBody);
		
        SoundManager.PlayOneShot(Sound_Explode);

        DeactivateShield(false);

        Invoke("ShowGameOverMenu", 3f);
        return PlayerState.DEAD;
		
	}

    private void ShowGameOverMenu() 
	{
        iTween.Stop();
        GlobalPreferences.SaveScoresToDisk(this, false);

        //Update Score label
        if (Score > HighScore)
        {
            dfLabel ScoreLabel = (dfLabel) GameOverMenu.Find("ScoreLabel");
            ScoreLabel.BottomColor = Color.yellow;
            ScoreLabel.Text = "High Score!";
        }
        
        GameOverMenu.Show();
	}
	
	private void CheckKeyboardInput() 
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

	private void CheckTouchInput ()
	{
		//don't check input when the players is boosting.
		if(State == PlayerState.BOOSTING || State == PlayerState.DEACTIVATING_BOOST) { return; }
				
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
        else if(col.tag.Contains("Rock") || col.tag.Contains("Enemy"))
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
	    IncreasePickupCount(type);

	    switch (type)
	    {
	        case "Collectible_Fuel":
	            Fuel += CollectibleRewards.FUEL_GAIN;
	            if (transform.position.z > TUTORIAL_DISTANCE)
	            {
	                Score += CollectibleRewards.SCORE_FUEL;
	            }
	            break;
	        case "Collectible_Speed":
	            ActivateBoost();
	            if (transform.position.z > TUTORIAL_DISTANCE)
	            {
	                Score += CollectibleRewards.SCORE_SPEED;
	            }
	            break;
	        case "Collectible_Shield":
	            ActivateShield();
	            if (transform.position.z > TUTORIAL_DISTANCE)
	            {
	                Score += CollectibleRewards.SCORE_SHIELD;
	            }
	            break;
	    }
	}

    private void IncreasePickupCount(string type)
    {
        switch (type)
        {
            case "Collectible_Fuel":
                greenPickupCount++;
                break;
            case "Collectible_Speed":
                redPickupCount++;
                break;
            case "Collectible_Shield":
                bluePickupCount++;
                break;
        }
    }

    private void ActivateShield()
    {
        ActivePowerUp = PowerUps.SHIELD;
        SoundManager.PlayOneShot(Sound_ShieldActivate);
        Shield.SetActive(true);
    }

    private void DeactivateShield(bool playSound = true)
    {
        ActivePowerUp = PowerUps.NONE;
        if (playSound) { SoundManager.PlayOneShot(Sound_ShieldExplode); }
        Shield.SetActive(false);
    }

    private void Boost() 
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), speed * Time.deltaTime);
        BoostTimeTraveled -= Time.deltaTime;

        MovePlayerForward(1.75f);

        WobbleCamera();

        if (BoostTimeTraveled <= 0 && CanLand())
        {
            State = PlayerState.DEACTIVATING_BOOST;
            SoundManager.PlayOneShot(Sound_DeactivateBoost);
        }
    }

    private void MovePlayerForward(float speedFactor = 1f)
    {
        transform.Translate(0f, 0f, speedFactor * speed * Time.deltaTime);
    }

	private void ActivateBoost() 
    {
        BoostTimeTraveled = BOOST_TIME;
		State = PlayerState.BOOSTING;

        SoundManager.PlayOneShot(Sound_ActivateBoost);

		BoostInitialPos = transform.position;
		BoostInitialCameraPos = PlayerCamera.transform.localPosition;
		
		iTween.ShakeRotation(PlayerCamera, BoostIntensity, 5.5f); //Maybe not needed after particles are implemented
	}

	private void DeactivateBoost() 
    {
		transform.position = Vector3.MoveTowards(transform.position, new Vector3 (BoostInitialPos.x, BoostInitialPos.y, transform.position.z), speed * Time.deltaTime);
		ResetCamera();
		if (transform.position.x == BoostInitialPos.x && transform.position.y == BoostInitialPos.y) 
		{
			State = PlayerState.ALIVE;
            iTween.Stop(); //Stop all iTweens to prevent camera snap bug after landing from boost
		}
	}

	private bool CanLand()
	{
		Vector3 aheadPos = transform.position;
		aheadPos.z += LANDING_DISTANCE;

        RaycastHit hit;

	    if (!Physics.Raycast(aheadPos, -transform.up, out hit, 100)) return false;

	    return hit.transform.tag == "Tunnel";
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
		transform.localPosition = new Vector3(x, y, transform.position.z);
	}
}