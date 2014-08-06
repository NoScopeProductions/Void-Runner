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

    [HideInInspector]
    public float DistanceTraveled;

    public GUIText ScoreLabel;
    public GUIText DistanceLabel;
    public GUIText FuelLabel;
    public GUIText ShieldLabel;
    public GUIText BoostLabel;

    public GUITexture ScoreBox;
    public Texture2D HighScoreBox;

    public bool ClearHighScore;

	void OnEnable () 
    {
        iTween.FadeTo(gameObject, 1f, 0.5f);

        ScoreLabel.text = Mathf.Round(score).ToString();
        DistanceLabel.text = Mathf.Round(DistanceTraveled).ToString();
        FuelLabel.text = fuelPickupCount.ToString();
        ShieldLabel.text = shieldPickupCount.ToString();
        BoostLabel.text = boostPickupCount.ToString();

        WriteToDisk();
	}

    private void WriteToDisk()
    {
        SetHighScore();
        SetFuelCount();
        SetShieldCount();
        SetBoostCount();
        SetNumberPlaythroughs();

        PlayerPrefs.Save();
    }

    private void SetNumberPlaythroughs()
    {
        if (PlayerPrefs.HasKey(GlobalPreferences.PLAYTHROUGHS))
        {
            PlayerPrefs.SetInt(GlobalPreferences.PLAYTHROUGHS, PlayerPrefs.GetInt(GlobalPreferences.PLAYTHROUGHS) + 1);
        }
        else
        {
            PlayerPrefs.SetInt(GlobalPreferences.PLAYTHROUGHS, 1);
        }

    }

    private void SetHighScore()
    {
        if (ClearHighScore)
        {
            PlayerPrefs.DeleteKey(GlobalPreferences.HIGH_SCORE);
        }

        if (PlayerPrefs.HasKey(GlobalPreferences.HIGH_SCORE))
        {
            if (score > PlayerPrefs.GetFloat(GlobalPreferences.HIGH_SCORE))
            {
                //change score guitexture
                ScoreBox.texture = HighScoreBox;
                PlayerPrefs.SetFloat(GlobalPreferences.HIGH_SCORE, score);
            }
        }
        else
        {
            ScoreBox.texture = HighScoreBox;
            PlayerPrefs.SetFloat(GlobalPreferences.HIGH_SCORE, score);
        }
    }

    private void SetFuelCount()
    {
        if (PlayerPrefs.HasKey(GlobalPreferences.FUEL_PICKUPS))
        {
            PlayerPrefs.SetInt(GlobalPreferences.FUEL_PICKUPS, PlayerPrefs.GetInt(GlobalPreferences.FUEL_PICKUPS) + fuelPickupCount);
        }
        else
        {
            PlayerPrefs.SetInt(GlobalPreferences.FUEL_PICKUPS, fuelPickupCount);
        }
    }

    private void SetShieldCount()
    {
        if (PlayerPrefs.HasKey(GlobalPreferences.SHIELD_PICKUPS))
        {
            PlayerPrefs.SetInt(GlobalPreferences.SHIELD_PICKUPS, PlayerPrefs.GetInt(GlobalPreferences.SHIELD_PICKUPS) + shieldPickupCount);
        }
        else
        {
            PlayerPrefs.SetInt(GlobalPreferences.SHIELD_PICKUPS, shieldPickupCount);
        }
    }

    private void SetBoostCount()
    {
        if (PlayerPrefs.HasKey(GlobalPreferences.BOOST_PICKUPS))
        {
            PlayerPrefs.SetInt(GlobalPreferences.BOOST_PICKUPS, PlayerPrefs.GetInt(GlobalPreferences.BOOST_PICKUPS) + boostPickupCount);
        }
        else
        {
            PlayerPrefs.SetInt(GlobalPreferences.BOOST_PICKUPS, boostPickupCount);
        }
    }
}