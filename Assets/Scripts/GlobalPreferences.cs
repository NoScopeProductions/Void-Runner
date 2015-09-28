using UnityEngine;
using System.Collections;
using Parse;

public class GlobalPreferences : MonoBehaviour 
{
    public enum SHIP { REMAKER, DSK, DEFAULT };

	public static SHIP shipSelected = SHIP.DEFAULT;

    public static bool SkipTutorial = false;
    public static bool gamePaused = false;

    public static float CurrentTimeScale = 1f;

    //PlayerPref Keys

    public static string HIGH_SCORE = "HighScore";
    public static string FUEL_PICKUPS = "FuelPickups";
    public static string SHIELD_PICKUPS = "ShieldPickups";
    public static string BOOST_PICKUPS = "BoostPickups";
    public static string PLAYTHROUGHS = "NumberOfPlaythroughs";
    public static string LAST_HIGHSCORE_NAME = "LastHighScoreName";

    public static string LastHighScoreName = "";

	void Awake()
	{
		DontDestroyOnLoad (this);
        LastHighScoreName = PlayerPrefs.HasKey(LAST_HIGHSCORE_NAME) ? PlayerPrefs.GetString(LAST_HIGHSCORE_NAME) : "";
    }

    public static void SetDefaultTimeScale()
    {
        Time.timeScale = 1f;
        CurrentTimeScale = 1f;
    }

    public static void SetTimeScale(float newScale)
    {
        Time.timeScale = newScale;
        CurrentTimeScale = newScale;
    }

    public static void TogglePause()
    {
        gamePaused = !gamePaused;

//        var playPauseButton = GameObject.Find("PlayPauseButton");

        //var checkbox = playPauseButton.GetComponent<dfCheckbox>();

        if (gamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = GlobalPreferences.CurrentTimeScale;
            //TODO: Tween timescale from 0 to current
        }
    }

    public static void SaveScoresToDisk(Player PlayerObject, bool ClearHighScore)
    {
        SetHighScore(PlayerObject, ClearHighScore);
        SetFuelCount(PlayerObject);
        SetShieldCount(PlayerObject);
        SetBoostCount(PlayerObject);
        SetNumberPlaythroughs();
        SaveLastHighScoreName();

        PlayerPrefs.Save();
    }

    private static void SaveLastHighScoreName()
    {
        PlayerPrefs.SetString(LAST_HIGHSCORE_NAME, LastHighScoreName);
    }

    private static void SetNumberPlaythroughs()
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

    private static void SetHighScore(Player PlayerObject, bool ClearHighScore)
    {
        if (ClearHighScore)
        {
            PlayerPrefs.DeleteKey(GlobalPreferences.HIGH_SCORE);
        }

        if (PlayerPrefs.HasKey(GlobalPreferences.HIGH_SCORE))
        {
            if (PlayerObject.Score > PlayerPrefs.GetFloat(GlobalPreferences.HIGH_SCORE))
            {
                PlayerPrefs.SetFloat(GlobalPreferences.HIGH_SCORE, PlayerObject.Score);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(GlobalPreferences.HIGH_SCORE, PlayerObject.Score);
        }
    }

    private static void SetFuelCount(Player PlayerObject)
    {
        if (PlayerPrefs.HasKey(GlobalPreferences.FUEL_PICKUPS))
        {
            PlayerPrefs.SetInt(GlobalPreferences.FUEL_PICKUPS, PlayerPrefs.GetInt(GlobalPreferences.FUEL_PICKUPS) + PlayerObject.greenPickupCount);
        }
        else
        {
            PlayerPrefs.SetInt(GlobalPreferences.FUEL_PICKUPS, PlayerObject.greenPickupCount);
        }
    }

    private static void SetShieldCount(Player PlayerObject)
    {
        if (PlayerPrefs.HasKey(GlobalPreferences.SHIELD_PICKUPS))
        {
            PlayerPrefs.SetInt(GlobalPreferences.SHIELD_PICKUPS, PlayerPrefs.GetInt(GlobalPreferences.SHIELD_PICKUPS) + PlayerObject.bluePickupCount);
        }
        else
        {
            PlayerPrefs.SetInt(GlobalPreferences.SHIELD_PICKUPS, PlayerObject.bluePickupCount);
        }
    }

    private static void SetBoostCount(Player PlayerObject)
    {
        if (PlayerPrefs.HasKey(GlobalPreferences.BOOST_PICKUPS))
        {
            PlayerPrefs.SetInt(GlobalPreferences.BOOST_PICKUPS, PlayerPrefs.GetInt(GlobalPreferences.BOOST_PICKUPS) + PlayerObject.redPickupCount);
        }
        else
        {
            PlayerPrefs.SetInt(GlobalPreferences.BOOST_PICKUPS, PlayerObject.redPickupCount);
        }
    }
}
