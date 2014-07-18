using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour 
{
    [HideInInspector]
    public float score;

    [HideInInspector]
    public int boostPickupCount;

    [HideInInspector]
    public int fuelPickupCount;

    [HideInInspector]
    public int shieldPickupCount;

    public GUIText ScoreLabel;
    public GUIText FuelLabel;
    public GUIText ShieldLabel;
    public GUIText BoostLabel;

	// Use this for initialization
	void OnEnable () 
    {
        ScoreLabel.text = "Score: " + Mathf.Round(score).ToString();
        FuelLabel.text = "Fuel: " + fuelPickupCount;
        ShieldLabel.text = "Shields: " + shieldPickupCount;
        BoostLabel.text = "Boosts: " + boostPickupCount;

        WriteToDisk();
	}

    private void WriteToDisk()
    {
        SetHighScore();
        SetFuelCount();
        SetShieldCount();
        SetBoostCount();

        PlayerPrefs.Save();
    }

    private void SetHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (score > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }
    }

    private void SetFuelCount()
    {
        if (PlayerPrefs.HasKey("FuelPickups"))
        {
            PlayerPrefs.SetInt("FuelPickups", PlayerPrefs.GetInt("Fuelpickups") + fuelPickupCount);
        }
        else
        {
            PlayerPrefs.SetInt("FuelPickups", fuelPickupCount);
        }
    }

    private void SetShieldCount()
    {
        if (PlayerPrefs.HasKey("ShieldPickups"))
        {
            PlayerPrefs.SetInt("ShieldPickups", PlayerPrefs.GetInt("ShieldPickups") + shieldPickupCount);
        }
        else
        {
            PlayerPrefs.SetInt("ShieldPickups", shieldPickupCount);
        }
    }

    private void SetBoostCount()
    {
        if (PlayerPrefs.HasKey("BoostPickups"))
        {
            PlayerPrefs.SetInt("BoostPickups", PlayerPrefs.GetInt("BoostPickups") + boostPickupCount);
        }
        else
        {
            PlayerPrefs.SetInt("BoostPickups", boostPickupCount);
        }
    }
}
