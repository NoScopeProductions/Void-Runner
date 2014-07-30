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

        //change score text if new high score

        WriteToDisk();
	}

    private void WriteToDisk()
    {
        SetHighScore();
        SetFuelCount();
        SetShieldCount();
        SetBoostCount();
        SetDistanceTraveled();

        PlayerPrefs.Save();
    }

    private void SetHighScore()
    {
        if (ClearHighScore)
        {
            PlayerPrefs.DeleteKey("HighScore");
        }

        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (score > PlayerPrefs.GetFloat("HighScore"))
            {
                //change score guitexture
                ScoreBox.texture = HighScoreBox;
                PlayerPrefs.SetFloat("HighScore", score);
            }
        }
        else
        {
            ScoreBox.texture = HighScoreBox;
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

    private void SetDistanceTraveled()
    {
        if (PlayerPrefs.HasKey("DistanceTraveled"))
        {
            PlayerPrefs.SetFloat("DistanceTraveled", PlayerPrefs.GetFloat("DistanceTraveled") + DistanceTraveled);
        }
        else
        {
            PlayerPrefs.SetFloat("DistanceTraveled", DistanceTraveled);
        }
    }
}
