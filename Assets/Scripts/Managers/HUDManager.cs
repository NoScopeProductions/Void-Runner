using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour 
{
	public Texture2D FuelBarGreen;
	public Texture2D FuelBarOrange;
	public Texture2D FuelBarRed;

    public GUITexture FuelBar;
    private Vector3 FuelBarPosition;

    private enum FuelBarColors { RED, ORANGE, GREEN};
    private FuelBarColors FuelBarColor;


    public GUIText ScoreText;

    public Player PlayerObject;

    public GameOverMenu endGameMenu;

    public GameObject[] DisableOnDeath;

	// Use this for initialization
	void Start () 
	{
        FuelBarPosition = new Vector3(0.5f, 1f, -1f);
        FuelBarColor = FuelBarColors.GREEN;
        
	}
	
	// Update is called once per frame
	void Update () 
	{
        UpdateScore();
        UpdateFuel();
	}

    private void UpdateFuel()
    {
        FuelBarPosition.x = Mathf.Clamp(PlayerObject.Fuel / 100f - 0.5f, -0.5f, 0.5f);
        FuelBar.transform.position = FuelBarPosition;

        if (PlayerObject.Fuel < 30)
        {
            if (FuelBarColor != FuelBarColors.RED)
            {
                FuelBar.guiTexture.texture = FuelBarRed;
                FuelBarColor = FuelBarColors.RED;
            }
        }
        else if (PlayerObject.Fuel < 60)
        {
            FuelBar.guiTexture.texture = FuelBarOrange;
            FuelBarColor = FuelBarColors.ORANGE;
        }
        else
        {
            FuelBar.guiTexture.texture = FuelBarGreen;
            FuelBarColor = FuelBarColors.GREEN;
        }
    }

    public void ShowGameOverMenu()
    {
        endGameMenu.score = PlayerObject.Score;
        endGameMenu.boostPickupCount = PlayerObject.redPickupCount;
        endGameMenu.fuelPickupCount = PlayerObject.greenPickupCount;
        endGameMenu.shieldPickupCount = PlayerObject.bluePickupCount;
        endGameMenu.DistanceTraveled = PlayerObject.DistanceTraveled;

        endGameMenu.gameObject.SetActive(true);
        ScoreText.gameObject.SetActive(false);

        foreach (var obj in DisableOnDeath)
        {
            obj.SetActive(false);
        }

    }
	
	void UpdateScore()
	{
        ScoreText.text = "Score: " + Mathf.Round(PlayerObject.Score).ToString();
	}
}
