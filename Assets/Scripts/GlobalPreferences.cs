using UnityEngine;
using System.Collections;

public class GlobalPreferences : MonoBehaviour 
{
    public enum SHIP { REMAKER, DSK, DEFAULT };

	public enum GameState { MAIN_MENU, SELECTION, PLAYING, PAUSED, GAME_OVER, CREDITS, STATS };

	public static GameState currentState = GameState.MAIN_MENU;

	public static SHIP shipSelected = SHIP.DEFAULT;

    public static bool SkipTutorial = false;

    public static float CurrentTimeScale = 1f;

    //PlayerPref Keys

    public static string HIGH_SCORE = "HighScore";
    public static string FUEL_PICKUPS = "FuelPickups";
    public static string SHIELD_PICKUPS = "ShieldPickups";
    public static string BOOST_PICKUPS = "BoostPickups";
    public static string PLAYTHROUGHS = "NumberOfPlaythroughs";

	void Awake()
	{
		DontDestroyOnLoad (this);
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



    public static void SaveScoresToDisk(Player PlayerObject, bool ClearHighScore)
    {
        SetHighScore(PlayerObject, ClearHighScore);
        SetFuelCount(PlayerObject);
        SetShieldCount(PlayerObject);
        SetBoostCount(PlayerObject);
        SetNumberPlaythroughs();

        PlayerPrefs.Save();
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
