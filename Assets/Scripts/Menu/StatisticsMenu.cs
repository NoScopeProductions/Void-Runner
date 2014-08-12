using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatisticsMenu : MonoBehaviour 
{

    public GUIText HighScore;
    public GUIText NumberOfGames;
    public GUIText FuelPickups;
    public GUIText ShieldPickups;
    public GUIText BoostPickups;


	// Use this for initialization
	void OnEnable () 
	{
        iTween.FadeTo(gameObject, 1f, 0.5f);

        ReadFromDisk();
	}

    private void ReadFromDisk()
    {
        HighScore.text = PlayerPrefs.HasKey(GlobalPreferences.HIGH_SCORE) ? Mathf.Round(PlayerPrefs.GetFloat(GlobalPreferences.HIGH_SCORE)).ToString() : "0";
        NumberOfGames.text = PlayerPrefs.HasKey(GlobalPreferences.PLAYTHROUGHS) ? PlayerPrefs.GetInt(GlobalPreferences.PLAYTHROUGHS).ToString() : "0";
        FuelPickups.text = PlayerPrefs.HasKey(GlobalPreferences.FUEL_PICKUPS) ? PlayerPrefs.GetInt(GlobalPreferences.FUEL_PICKUPS).ToString() : "0";
        ShieldPickups.text = PlayerPrefs.HasKey(GlobalPreferences.SHIELD_PICKUPS) ? PlayerPrefs.GetInt(GlobalPreferences.SHIELD_PICKUPS).ToString() : "0";
        BoostPickups.text = PlayerPrefs.HasKey(GlobalPreferences.BOOST_PICKUPS) ? PlayerPrefs.GetInt(GlobalPreferences.BOOST_PICKUPS).ToString() : "0";
    }
}
