using UnityEngine;
using System.Collections;

public class SetOverallStats : MonoBehaviour 
{
    public dfTextbox HighScore;
    public dfTextbox NumberOfGames;
    public dfLabel BoostPickups;
    public dfLabel FuelPickups;
    public dfLabel ShieldPickups;

    void OnEnable()
    {
        ReadFromDisk();
    }

    private void ReadFromDisk()
    {
        HighScore.Text = PlayerPrefs.HasKey(GlobalPreferences.HIGH_SCORE) ? Mathf.Round(PlayerPrefs.GetFloat(GlobalPreferences.HIGH_SCORE)).ToString() : "0";
        NumberOfGames.Text = PlayerPrefs.HasKey(GlobalPreferences.PLAYTHROUGHS) ? PlayerPrefs.GetInt(GlobalPreferences.PLAYTHROUGHS).ToString() : "0";
        FuelPickups.Text = PlayerPrefs.HasKey(GlobalPreferences.FUEL_PICKUPS) ? PlayerPrefs.GetInt(GlobalPreferences.FUEL_PICKUPS).ToString() : "0";
        ShieldPickups.Text = PlayerPrefs.HasKey(GlobalPreferences.SHIELD_PICKUPS) ? PlayerPrefs.GetInt(GlobalPreferences.SHIELD_PICKUPS).ToString() : "0";
        BoostPickups.Text = PlayerPrefs.HasKey(GlobalPreferences.BOOST_PICKUPS) ? PlayerPrefs.GetInt(GlobalPreferences.BOOST_PICKUPS).ToString() : "0";
    }
}
